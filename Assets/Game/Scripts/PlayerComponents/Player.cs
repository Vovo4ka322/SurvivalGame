using System;
using UnityEngine;
using Game.Scripts.MenuComponents.ShopComponents.WalletComponents;
using Game.Scripts.PlayerComponents.Controller;

namespace Game.Scripts.PlayerComponents
{
    public class Player : Character
    {
        [SerializeField] private float _baseHealth;
        [SerializeField] private float _baseArmor;
        [SerializeField] private float _baseDamage;
        [SerializeField] private float _baseAttackSpeed;
        [SerializeField] private float _baseMoveSpeed;
        [SerializeField] private PlayerLevel _level = new();

        private Wallet _money;

        public event Action<float> HealthChanged;
        public event Action<float> ExperienceChanged;
        public event Action<int> MoneyChanged;
        public event Action Death;

        [field:SerializeField] public CharacterType CharacterType { get; private set; }

        public PlayerLevel Level => _level;

        [field: SerializeField] public PlayerMovement PlayerMovement {  get; private set; }

        [field: SerializeField] public float GeneralHealth { get; private set; }

        [field: SerializeField] public float GeneralArmor { get; private set; }

        [field: SerializeField] public float GeneralDamage { get; private set; }

        [field: SerializeField] public float GeneralAttackSpeed { get; private set; }

        [field: SerializeField] public float GeneralMovementSpeed { get; private set; }

        public void Init(float health, float armor, float damage, float attackSpeed, float movementSpeed, Wallet money)
        {
            GeneralHealth = _baseHealth + health;
            GeneralArmor = _baseArmor + armor;
            GeneralDamage = _baseDamage + damage;
            GeneralAttackSpeed = _baseAttackSpeed + attackSpeed;
            GeneralMovementSpeed = _baseMoveSpeed + movementSpeed;

            _money = money;
            _level.Init();
            Health.InitMaxValue(GeneralHealth);
            PlayerMovement.Init(GeneralMovementSpeed);
        }

        public void GetMoney(int value)
        {
            _money.AddCoins(value);
            MoneyChanged?.Invoke(value);
        }

        public void GetExperience(int value)
        {
            _level.GainExperience(value);
            ExperienceChanged?.Invoke(value);
        }

        protected void AddHealth(float value)
        {
            Health.Add(value);
            HealthChanged?.Invoke(Health.Value);
        }

        public void LoseHealth(float value)
        {
            Health.Lose(value);
            HealthChanged.Invoke(Health.Value);

            if (Health.IsDead)
                Death?.Invoke();
        }
    }
}