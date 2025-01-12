using System;
using System.Collections.Generic;
using Ability.MeleeAbilities.BladeFury;
using Ability.MeleeAbilities.BorrowedTime;
using PlayerComponents;
using UnityEngine;

namespace Ability.MeleeAbilities
{
    public class MeleeAbilityUser : MonoBehaviour, IAbilityUser
    {
        [SerializeField] private MeleePlayer _player;
        [SerializeField] private BorrowedTimeUser _borrowedTime;
        [SerializeField] private BladeFuryUser _bladeFury;

        [Header("Level of abilities")]
        [SerializeField] private MeleeAbilityData _abilityDataFirstLevel;
        [SerializeField] private MeleeAbilityData _abilityDataSecondLevel;
        [SerializeField] private MeleeAbilityData _abilityDataThirdLevel;

        private int _firstLevel = 1;
        private int _secondLevel = 2;
        private int _thirdLevel = 3;

        private int _counterForBorrowedTime = 0;
        private int _counterForBladeFury = 0;
        private int _counterForBloodlust = 0;

        private int _firstUpgrade = 0;
        private int _secondUpgrade = 1;
        private int _thirdUpgrade = 2;

        private Dictionary<int, MeleeAbilityData> _abilitiesDatas;

        public event Action LevelChanged;
        public event Action BladeFuryUpgraded;
        public event Action BorrowedTimeIUpgraded;
        public event Action BloodlustIUpgraded;

        public int MaxValue { get; private set; } = 3;

        public BorrowedTimeUser BorrowedTime => _borrowedTime;

        public BladeFuryUser BladeFury => _bladeFury;

        private void Awake()
        {
            _abilitiesDatas = new Dictionary<int, MeleeAbilityData>
            {
                { _firstLevel, _abilityDataFirstLevel },
                { _secondLevel, _abilityDataSecondLevel },
                { _thirdLevel, _abilityDataThirdLevel }
            };
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

        public void UseFirstAbility()
        {
            StartCoroutine(_bladeFury.UseAbility(_player.transform));
        }

        public void UseSecondAbility()
        {
            StartCoroutine(_borrowedTime.UseAbility(_player));
        }

        public void UpgradeFirstAbility()
        {
            if (IsTrue(_counterForBladeFury, _firstUpgrade))
                UpgradeBladeFury(_firstLevel);
            else if (IsTrue(_counterForBladeFury, _secondUpgrade))
                UpgradeBladeFury(_secondLevel);
            else if (IsTrue(_counterForBladeFury, _thirdUpgrade))
                UpgradeBladeFury(_thirdLevel);
        }

        public void UpgradeSecondAbility()
        {
            if (IsTrue(_counterForBorrowedTime, _firstUpgrade))
                UpgradeBorrowedTime(_firstLevel);
            else if (IsTrue(_counterForBorrowedTime, _secondUpgrade))
                UpgradeBorrowedTime((_secondLevel));
            else if (IsTrue(_counterForBorrowedTime, _thirdUpgrade))
                UpgradeBorrowedTime(_thirdLevel);
        }

        public void UpgradeThirdAbility()
        {
            if (IsTrue(_counterForBloodlust, _firstUpgrade))
                UpgradeBloodlust(_firstLevel);
            if (IsTrue(_counterForBloodlust, _secondUpgrade))
                UpgradeBloodlust(_secondLevel);
            if (IsTrue(_counterForBloodlust, _thirdLevel))
                UpgradeBloodlust(_thirdLevel);
        }

        private bool IsTrue(int counter, int numberOfUpgrade) => counter == numberOfUpgrade;

        public bool IsMaxValue(int value) => value == MaxValue;

        private void UpgradeBladeFury(int level)
        {
            if (IsMaxValue(_counterForBladeFury))
                return;

            _bladeFury.Upgrade(_abilitiesDatas[level].BladeFuryScriptableObject);
            _counterForBladeFury++;
            BladeFuryUpgraded?.Invoke();
        }

        private void UpgradeBorrowedTime(int level)
        {
            if (IsMaxValue(_counterForBorrowedTime))
                return;

            _borrowedTime.Upgrade(_abilitiesDatas[level].BorrowedTimeScriptableObject);
            _counterForBorrowedTime++;
            BorrowedTimeIUpgraded?.Invoke();
        }

        private void UpgradeBloodlust(int level)
        {
            if (IsMaxValue(_counterForBloodlust))
                return;

            _player.UpgradeCharacteristikByBloodlust(_abilitiesDatas[level].BloodlustScriptableObject);
            _counterForBloodlust++;
            BloodlustIUpgraded?.Invoke();
        }
    }
}