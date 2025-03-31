using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.AbilityComponents.MeleeAbilities.BladeFuryComponents;
using Game.Scripts.AbilityComponents.MeleeAbilities.BorrowedTimeComponents;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    public class MeleePlayerAbility : AbilityUserBase<AbilitySet>, IAbilityUser
    {
        [SerializeField] private MeleePlayer _player;
        [SerializeField] private BorrowedTimeAbility _borrowedTime;
        [SerializeField] private BladeFuryAbility _bladeFury;
        [SerializeField] private AbilitySet _abilityDataFirstLevel;
        [SerializeField] private AbilitySet _abilityDataSecondLevel;
        [SerializeField] private AbilitySet _abilityDataThirdLevel;
        
        private int _counterForBladeFury = 0;
        private int _counterForBorrowedTime = 0;
        private int _counterForBloodLust = 0;
        
        public event Action BladeFuryUpgraded;
        public event Action BorrowedTimeIUpgraded;
        public event Action BloodLustIUpgraded;
        
        public BorrowedTimeAbility BorrowedTime => _borrowedTime;
        public BladeFuryAbility BladeFury => _bladeFury;
        public int CurrentBladeFuryLevel => _counterForBladeFury;
        public int CurrentBorrowedTimeLevel => _counterForBorrowedTime;
        public int CurrentBloodLustLevel => _counterForBloodLust;
        
        private void Awake()
        {
            AbilitiesDatas = new Dictionary<int, AbilitySet> { { FirstLevel, _abilityDataFirstLevel }, { SecondLevel, _abilityDataSecondLevel }, { ThirdLevel, _abilityDataThirdLevel }, };
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
            TryUpgradeAbility(ref _counterForBladeFury, FirstLevel, SecondLevel, ThirdLevel, data => data.BladeFury, _bladeFury.ReplaceValue, BladeFuryUpgraded);
        }
        
        public override void UpgradeSecondAbility()
        {
            TryUpgradeAbility(ref _counterForBorrowedTime, FirstLevel, SecondLevel, ThirdLevel, data => data.BorrowedTime, _borrowedTime.ReplaceValue, BorrowedTimeIUpgraded);
        }
        
        public override void UpgradeThirdAbility()
        {
            TryUpgradeAbility(ref _counterForBloodLust, FirstLevel, SecondLevel, ThirdLevel, data => data.BloodLust, scriptableObject => _player.UpgradeCharacteristikByBloodLust(scriptableObject), BloodLustIUpgraded);
        }
    }
}