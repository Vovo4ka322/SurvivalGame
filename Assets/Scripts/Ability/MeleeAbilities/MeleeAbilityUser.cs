using System;
using UnityEngine;
using MainPlayer;
using System.Collections.Generic;

namespace Abilities
{
    public class MeleeAbilityUser : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private BorrowedTimeUser _borrowedTime;
        [SerializeField] private BladeFuryUser _bladeFury;

        [Header("Level of abilities")]
        [SerializeField] private AbilityData _abilityDataFirstLevel;
        [SerializeField] private AbilityData _abilityDataSecondLevel;
        [SerializeField] private AbilityData _abilityDataThirdLevel;

        private int _firstLevel = 1;
        private int _secondLevel = 2;
        private int _thirdLevel = 3;

        private int _counterForBorrowedTime = 0;
        private int _counterForBladeFury = 0;
        private int _counterForBloodlust = 0;

        private int _firstUpgrade = 0;
        private int _secondUpgrade = 1;
        private int _thirdUpgrade = 2;

        private Dictionary<int, AbilityData> _abilitiesDatas;

        public event Action LevelChanged;
        public event Action AbilityUpgraded;

        private void Awake()
        {
            _abilitiesDatas = new Dictionary<int, AbilityData>
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

        public void OpenUpgraderWindow()
        {
            LevelChanged?.Invoke();
        }

        public void UseBladeFury()
        {
            StartCoroutine(_bladeFury.UseAbility(_player.transform));
        }

        public void UseBorrowedTime()
        {
            StartCoroutine(_borrowedTime.UseAbility(_player));
        }

        public void UpgradeBladeFury()
        {
            if (IsTrue(_counterForBladeFury, _firstUpgrade))
                UpgradeBladeFury(_firstLevel);
            else if(IsTrue(_counterForBladeFury, _secondUpgrade))
                UpgradeBladeFury(_secondLevel);
            else if(IsTrue(_counterForBladeFury, _thirdUpgrade))
                UpgradeBladeFury(_thirdLevel);
        }

        public void UpgradeBorrowedTime()
        {
            if (IsTrue(_counterForBorrowedTime, _firstUpgrade))
                UpgradeBorrowedTime(_firstLevel);
            else if (IsTrue(_counterForBorrowedTime, _secondUpgrade))
                UpgradeBorrowedTime((_secondLevel));
            else if (IsTrue(_counterForBorrowedTime, _thirdUpgrade))
                UpgradeBorrowedTime(_thirdLevel);
        }

        public void UpgradeBloodlust()
        {
            if(IsTrue(_counterForBloodlust, _firstUpgrade))
                UpgradeBloodlust(_firstLevel);
            if(IsTrue(_counterForBloodlust, _secondUpgrade))
                UpgradeBloodlust(_secondLevel);
            if (IsTrue(_counterForBloodlust, _thirdLevel))
                UpgradeBloodlust(_thirdLevel);
        }

        private bool IsTrue(int counter, int numberOfUpgrade) => counter == numberOfUpgrade;

        private void UpgradeBladeFury(int level)
        {
            _bladeFury.Upgrade(_abilitiesDatas[level]._bladeFuryScriptableObject);
            _counterForBladeFury++;
            AbilityUpgraded?.Invoke();
            Debug.Log(_counterForBladeFury + "123");
        }

        private void UpgradeBorrowedTime(int level)
        {
            _borrowedTime.Upgrade(_abilitiesDatas[level]._borrowedTimeScriptableObject);
            _counterForBorrowedTime++;
            AbilityUpgraded?.Invoke();
        }

        private void UpgradeBloodlust(int level)
        {
            _player.UpgradeCharacteristikByBloodlust(_abilitiesDatas[level]._bloodlustScriptableObject);
            _counterForBloodlust++;
            AbilityUpgraded?.Invoke();
        }
    }
}