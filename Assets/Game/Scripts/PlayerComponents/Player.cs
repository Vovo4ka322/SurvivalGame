using System;
using UnityEngine;
using Game.Scripts.MenuComponents.ShopComponents.WalletComponents;
using Game.Scripts.PlayerComponents.Controller;
using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MusicComponents.EffectSounds;
using Game.Scripts.PlayerComponents.Animations;
using YG;

namespace Game.Scripts.PlayerComponents
{
    public class Player : Character
    {
        private const string NameLeaderboard = "LeaderBoard";
        private const string MaxScore = "MaxScore";

        [SerializeField] private PlayerLevel _level = new ();
        [SerializeField] private float _baseHealth;
        [SerializeField] private float _baseArmor;
        [SerializeField] private float _baseDamage;
        [SerializeField] private float _baseAttackSpeed;
        [SerializeField] private float _baseMoveSpeed;

        private SoundCollection _soundCollection;
        private IDataSaver _dataSaver;
        private Wallet _wallet;

        private int _currentRunScore = 0;

        public event Action<float> HealthChanged;
        public event Action<float> ExperienceChanged;
        public event Action<int> MoneyChanged;
        public event Action Death;

        [field: SerializeField] public CharacterType CharacterType { get; private set; }
        [field: SerializeField] public AnimatorStatePlayer AnimatorState { get; private set; }
        [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
        [field: SerializeField] public float GeneralHealth { get; private set; }
        [field: SerializeField] public float GeneralArmor { get; private set; }
        [field: SerializeField] public float GeneralDamage { get; private set; }
        [field: SerializeField] public float GeneralAttackSpeed { get; private set; }
        [field: SerializeField] public float GeneralMovementSpeed { get; private set; }
        public PlayerLevel Level => _level;
        public SoundCollection SoundCollection => _soundCollection;

        public void Init(float health, float armor, float damage, float attackSpeed, float movementSpeed, Wallet wallet, IDataSaver saver)
        {
            GeneralHealth = _baseHealth + health;
            GeneralArmor = _baseArmor + armor;
            GeneralDamage = _baseDamage + damage;
            GeneralAttackSpeed = _baseAttackSpeed + attackSpeed;
            GeneralMovementSpeed = _baseMoveSpeed + movementSpeed;

            _dataSaver = saver;
            _wallet = wallet;

            _level.Init();
            GeneralHealth += GeneralArmor;
            Health.InitMaxValue(GeneralHealth);
            PlayerMovement.Init(GeneralMovementSpeed);
        }

        public void InitJoysticks(bool isJoystickActive, Joystick joystickForMovement, Joystick joystickForRotation)
        {
            PlayerMovement.InitJoysticks(isJoystickActive, joystickForMovement, joystickForRotation);
        }

        public void SetSoundCollection(SoundCollection soundCollection)
        {
            _soundCollection = soundCollection;
        }

        public void AddPoints(int points)
        {
            _currentRunScore += points;

            int storedMaxScore = PlayerPrefs.GetInt(MaxScore, 0);

            if (_currentRunScore > storedMaxScore)
            {
                PlayerPrefs.SetInt(MaxScore, _currentRunScore);
                PlayerPrefs.Save();
                YandexGame.NewLeaderboardScores(NameLeaderboard, _currentRunScore);
            }
        }

        public void GetMoney(int value)
        {
            _wallet.AddCoins(value);

            MoneyChanged?.Invoke(_wallet.GetCurrentCoins());

            _dataSaver.Save();
        }

        public void GetExperience(int value)
        {
            _level.GainExperience(value);

            ExperienceChanged?.Invoke(_level.Experience);
        }

        protected void ChangeMovementAnimationSpeed(int id, float value) => AnimatorState.SetFloatValue(id, value);

        protected void ChangeAttackAnimationSpeed(int id, float value) => AnimatorState.SetFloatValue(id, value);

        protected void AddHealth(float value)
        {
            Health.Add(value);

            HealthChanged?.Invoke(Health.Value);
        }

        protected void LoseHealth(float value)
        {
            Health.Lose(value);

            HealthChanged.Invoke(Health.Value);

            if (Health.IsDead)
            {
                Death?.Invoke();
            }
        }
    }
}