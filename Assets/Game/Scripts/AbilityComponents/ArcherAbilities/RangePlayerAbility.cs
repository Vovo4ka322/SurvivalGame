using Game.Scripts.AbilityComponents.ArcherAbilities.InsatiableHungerAbility;
using Game.Scripts.AbilityComponents.ArcherAbilities.MultiShotAbility;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents;
using Game.Scripts.Weapons.RangedWeapon;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.AbilityComponents.ArcherAbilities
{
    public class RangePlayerAbility : MonoBehaviour, IAbilityUser
    {
        [SerializeField] private RangePlayer _player;
        [SerializeField] private Bow _bow;
        [SerializeField] private MultiShotAbility.MultiShotAbility _multiShot;
        [SerializeField] private InsatiableHungerAbility.InsatiableHungerAbility _insatiableHunger;

        [Header("Level of abilities")]
        [SerializeField] private RangeAbilitySetter _abilityDataFirstLevel;
        [SerializeField] private RangeAbilitySetter _abilityDataSecondLevel;
        [SerializeField] private RangeAbilitySetter _abilityDataThirdLevel;

        private int _counterForInsatiableHunger = 0;
        private int _counterForMultiShot = 0;
        private int _counterForBlur = 0;

        private Dictionary<int, RangeAbilitySetter> _abilitiesDatas;

        private readonly int _firstLevel = 1;
        private readonly int _secondLevel = 2;
        private readonly int _thirdLevel = 3;
        private readonly int _firstUpgrade = 0;
        private readonly int _secondUpgrade = 1;
        private readonly int _thirdUpgrade = 2;

        public event Action LevelChanged;
        public event Action MultiShotUpgraded;
        public event Action InsatiableHungerUpgraded;
        public event Action BlurUpgraded;

        public int MaxValue { get; private set; } = 3;
        public MultiShotAbility.MultiShotAbility MultiShotUser => _multiShot;
        public InsatiableHungerAbility.InsatiableHungerAbility InsatiableHunger => _insatiableHunger;
        public int CurrentMultiShotLevel => _counterForMultiShot;
        public int CurrentInsatiableHunger => _counterForInsatiableHunger;
        public int CurrentBlurLevel => _counterForBlur;

        private void Awake()
        {
            _abilitiesDatas = new Dictionary<int, RangeAbilitySetter>
            {
                { _firstLevel, _abilityDataFirstLevel },
                { _secondLevel, _abilityDataSecondLevel },
                { _thirdLevel, _abilityDataThirdLevel },
            };

            _multiShot.SetHandler(_player);
        }

        private void OnEnable()
        {
            _player.Level.LevelChanged += OpenUpgraderWindow;
        }

        private void OnDisable()
        {
            _player.Level.LevelChanged -= OpenUpgraderWindow;
        }

        public RangeAbilitySetter GetAbilityDataForLevel(int level)
        {
            if (_abilitiesDatas.ContainsKey(level))
            {
                return _abilitiesDatas[level];
            }

            return null;
        }

        public void OpenUpgraderWindow()
        {
            LevelChanged?.Invoke();
        }

        public void UpgradeFirstAbility()
        {
            if (IsTrue(_counterForMultiShot, _firstUpgrade))
                UpgradeMultishot(_firstLevel);
            else if (IsTrue(_counterForMultiShot, _secondUpgrade))
                UpgradeMultishot(_secondLevel);
            else if (IsTrue(_counterForMultiShot, _thirdUpgrade))
                UpgradeMultishot(_thirdLevel);
        }

        public void UpgradeSecondAbility()
        {
            if (IsTrue(_counterForInsatiableHunger, _firstUpgrade))
                UpgradeInsatiableHunger(_firstLevel);
            else if (IsTrue(_counterForInsatiableHunger, _secondUpgrade))
                UpgradeInsatiableHunger(_secondLevel);
            else if (IsTrue(_counterForInsatiableHunger, _thirdUpgrade))
                UpgradeInsatiableHunger(_thirdLevel);
        }

        public void UpgradeThirdAbility()
        {
            if (IsTrue(_counterForBlur, _firstUpgrade))
                UpgradeBlur(_firstLevel);
            else if (IsTrue(_counterForBlur, _secondUpgrade))
                UpgradeBlur(_secondLevel);
            else if (IsTrue(_counterForBlur, _thirdUpgrade))
                UpgradeBlur(_thirdLevel);
        }

        public void UseFirstAbility()
        {
            StartCoroutine(_multiShot.UseAbility(_player.Damage));
        }

        public void UseSecondAbility()
        {
            StartCoroutine(_insatiableHunger.UseAbility(_player));
        }

        private bool IsMaxValue(int value) => value == MaxValue;

        private bool IsTrue(int counter, int numberOfUpgrade) => counter == numberOfUpgrade;

        private void UpgradeMultishot(int level)
        {
            if (IsMaxValue(_counterForMultiShot))
                return;

            _multiShot.ReplaceValue(_abilitiesDatas[level].MultiShotScriptableObject);
            _counterForMultiShot++;
            MultiShotUpgraded?.Invoke();
        }

        private void UpgradeInsatiableHunger(int level)
        {
            if (IsMaxValue(_counterForInsatiableHunger))
                return;

            _insatiableHunger.ReplaceValue(_abilitiesDatas[level].InsatiableHungerScriptableObject);
            _counterForInsatiableHunger++;
            InsatiableHungerUpgraded?.Invoke();
        }

        private void UpgradeBlur(int level)
        {
            if (IsMaxValue(_counterForBlur))
                return;

            _player.SetEvasion(_abilitiesDatas[level].BlurScriptableObject);
            _counterForBlur++;
            BlurUpgraded?.Invoke();
        }
    }
}