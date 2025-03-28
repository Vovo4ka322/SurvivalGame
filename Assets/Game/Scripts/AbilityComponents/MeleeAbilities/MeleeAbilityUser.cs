using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.AbilityComponents.MeleeAbilities.BladeFuryAbility;
using Game.Scripts.AbilityComponents.MeleeAbilities.BorrowedTimeAbility;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    public class MeleeAbilityUser : AbilityUserBase<MeleeAbilityData>, IAbilityUser
    {
        [SerializeField] private MeleePlayer _player;
        [SerializeField] private BorrowedTimeUser _borrowedTime;
        [SerializeField] private BladeFuryUser _bladeFury;
        [SerializeField] private MeleeAbilityData _abilityDataFirstLevel;
        [SerializeField] private MeleeAbilityData _abilityDataSecondLevel;
        [SerializeField] private MeleeAbilityData _abilityDataThirdLevel;
        
        private int _counterForBladeFury = 0;
        private int _counterForBorrowedTime = 0;
        private int _counterForBloodLust = 0;
        
        public event Action BladeFuryUpgraded;
        public event Action BorrowedTimeIUpgraded;
        public event Action BloodLustIUpgraded;
        
        public BorrowedTimeUser BorrowedTime => _borrowedTime;
        public BladeFuryUser BladeFury => _bladeFury;
        public int CurrentBladeFuryLevel => _counterForBladeFury;
        public int CurrentBorrowedTimeLevel => _counterForBorrowedTime;
        public int CurrentBloodLustLevel => _counterForBloodLust;
        
        private void Awake()
        {
            AbilitiesDatas = new Dictionary<int, MeleeAbilityData> { { FirstLevel, _abilityDataFirstLevel }, { SecondLevel, _abilityDataSecondLevel }, { ThirdLevel, _abilityDataThirdLevel }, };
        }
        
        private void OnEnable()
        {
            _player.Level.LevelChanged += OpenUpgraderWindow;
        }
        
        private void OnDisable()
        {
            _player.Level.LevelChanged -= OpenUpgraderWindow;
        }
        
        public override void UseFirstAbility()
        {
            StartCoroutine(_bladeFury.UseAbility(_player));
        }
        
        public override void UseSecondAbility()
        {
            StartCoroutine(_borrowedTime.UseAbility(_player));
        }
        
        public override void UpgradeFirstAbility()
        {
            TryUpgradeAbility(ref _counterForBladeFury, FirstLevel, SecondLevel, ThirdLevel, data => data.BladeFuryScriptableObject, _bladeFury.Upgrade, BladeFuryUpgraded);
        }
        
        public override void UpgradeSecondAbility()
        {
            TryUpgradeAbility(ref _counterForBorrowedTime, FirstLevel, SecondLevel, ThirdLevel, data => data.BorrowedTimeScriptableObject, _borrowedTime.Upgrade, BorrowedTimeIUpgraded);
        }
        
        public override void UpgradeThirdAbility()
        {
            TryUpgradeAbility(ref _counterForBloodLust, FirstLevel, SecondLevel, ThirdLevel, data => data.BloodLustScriptableObject, scriptableObject => _player.UpgradeCharacteristikByBloodLust(scriptableObject), BloodLustIUpgraded);
        }
    }
}