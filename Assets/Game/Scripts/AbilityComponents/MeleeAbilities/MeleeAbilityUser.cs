using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.AbilityComponents.MeleeAbilities.BladeFuryAbility;
using Game.Scripts.AbilityComponents.MeleeAbilities.BorrowedTimeAbility;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
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

        private readonly int _firstLevel = 1;
        private readonly int _secondLevel = 2;
        private readonly int _thirdLevel = 3;
        private readonly int _firstUpgrade = 0;
        private readonly int _secondUpgrade = 1;
        private readonly int _thirdUpgrade = 2;

        private int _counterForBorrowedTime = 0;
        private int _counterForBladeFury = 0;
        private int _counterForBloodLust = 0;

        private Dictionary<int, MeleeAbilityData> _abilitiesDatas;

        public event Action LevelChanged;
        public event Action BladeFuryUpgraded;
        public event Action BorrowedTimeIUpgraded;
        public event Action BloodLustIUpgraded;

        public int MaxValue { get; private set; } = 3;

        public BorrowedTimeUser BorrowedTime => _borrowedTime;

        public BladeFuryUser BladeFury => _bladeFury;
        public int CurrentBladeFuryLevel => _counterForBladeFury;
        public int CurrentBorrowedTimeLevel => _counterForBorrowedTime;
        public int CurrentBloodLustLevel => _counterForBloodLust;

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
        
        public MeleeAbilityData GetAbilityDataForLevel(int level)
        {
            if(_abilitiesDatas.ContainsKey(level))
            {
                return _abilitiesDatas[level];
            }
            
            return null;
        }
        
        public void OpenUpgraderWindow()
        {
            LevelChanged?.Invoke();
        }

        public void UseFirstAbility()
        {
            StartCoroutine(_bladeFury.UseAbility(_player));
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
            if (IsTrue(_counterForBloodLust, _firstUpgrade))
                UpgradeBloodlust(_firstLevel);
            else if (IsTrue(_counterForBloodLust, _secondUpgrade))
                UpgradeBloodlust(_secondLevel);
            else if (IsTrue(_counterForBloodLust, _thirdUpgrade))
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
            if (IsMaxValue(_counterForBloodLust))
                return;

            _player.UpgradeCharacteristikByBloodlust(_abilitiesDatas[level].BloodLustScriptableObject);
            _counterForBloodLust++;
            BloodLustIUpgraded?.Invoke();
        }
    }
}