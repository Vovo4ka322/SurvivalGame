using System;
using System.Collections.Generic;
using UnityEngine;
using Ability.ArcherAbilities.InsatiableHunger;
using Ability.ArcherAbilities.Multishot;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents;
using Weapons.RangedWeapon;

namespace Ability.ArcherAbilities
{
    public class ArcherAbilityUser : MonoBehaviour, IAbilityUser
    {
        [SerializeField] private RangePlayer _player;
        [SerializeField] private Bow _bow;
        [SerializeField] private MultishotUser _multishot;
        [SerializeField] private InsatiableHungerUser _insatiableHunger;

        [Header("Level of abilities")]
        [SerializeField] private RangeAbilityData _abilityDataFirstLevel;
        [SerializeField] private RangeAbilityData _abilityDataSecondLevel;
        [SerializeField] private RangeAbilityData _abilityDataThirdLevel;

        private int _firstLevel = 1;
        private int _secondLevel = 2;
        private int _thirdLevel = 3;

        private int _counterForInsatiableHunger = 0;
        private int _counterForMultishot = 0;
        private int _counterForBlur = 0;

        private int _firstUpgrade = 0;
        private int _secondUpgrade = 1;
        private int _thirdUpgrade = 2;

        private Dictionary<int, RangeAbilityData> _abilitiesDatas;

        public event Action LevelChanged;
        public event Action MultishotUpgraded;
        public event Action InsatiableHungerUpgraded;
        public event Action BlurUpgraded;

        public int MaxValue { get; private set; } = 3;

        public MultishotUser MultishotUser => _multishot;

        public InsatiableHungerUser InsatiableHunger => _insatiableHunger;

        private void Awake()
        {
            _abilitiesDatas = new Dictionary<int, RangeAbilityData>
            {
                { _firstLevel, _abilityDataFirstLevel },
                { _secondLevel, _abilityDataSecondLevel },
                { _thirdLevel, _abilityDataThirdLevel }
            };

            _multishot.SetHandler(_player);
        }

        private void OnEnable()
        {
            _player.Level.LevelChanged += OpenUpgraderWindow;
        }

        private void OnDisable()
        {
            _player.Level.LevelChanged -= OpenUpgraderWindow;
        }

        public IAbilityUser Init() => this;

        public void OpenUpgraderWindow()
        {
            LevelChanged?.Invoke();
        }

        public void UpgradeFirstAbility()
        {
            if (IsTrue(_counterForMultishot, _firstUpgrade))
                UpgradeMultishot(_firstLevel);
            else if (IsTrue(_counterForMultishot, _secondUpgrade))
                UpgradeMultishot(_secondLevel);
            else if (IsTrue(_counterForMultishot, _thirdUpgrade))
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
            StartCoroutine(_multishot.UseAbility(_player.Damage));
        }

        public void UseSecondAbility()
        {
            StartCoroutine(_insatiableHunger.UseAbility(_player));
        }

        private bool IsMaxValue(int value) => value == MaxValue;

        private bool IsTrue(int counter, int numberOfUpgrade) => counter == numberOfUpgrade;

        private void UpgradeMultishot(int level)
        {
            if (IsMaxValue(_counterForMultishot))
                return;

            _multishot.Upgrade(_abilitiesDatas[level].Multishot);
            _counterForMultishot++;
            MultishotUpgraded?.Invoke();
        }

        private void UpgradeInsatiableHunger(int level)
        {
            if (IsMaxValue(_counterForInsatiableHunger))
                return;

            _insatiableHunger.Upgrade(_abilitiesDatas[level].InsatiableHunger);
            _counterForInsatiableHunger++;
            InsatiableHungerUpgraded?.Invoke();
        }

        private void UpgradeBlur(int level)
        {
            if(IsMaxValue(_counterForBlur))
                return;

            _player.SetEvasion(_abilitiesDatas[level].Blur);
            _counterForBlur++;
            BlurUpgraded?.Invoke();
        }
    }
}