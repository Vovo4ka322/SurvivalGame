using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.AbilityComponents.ArcherAbilities.InsatiableHungerAbility;
using Game.Scripts.AbilityComponents.ArcherAbilities.MultiShotAbility;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents;
using Game.Scripts.Weapons.RangedWeapon;

namespace Game.Scripts.AbilityComponents.ArcherAbilities
{
    public class ArcherAbilityUser : AbilityUserBase<RangeAbilityData>, IAbilityUser
    {
        [SerializeField] private RangePlayer _player;
        [SerializeField] private Bow _bow;
        [SerializeField] private MultiShotUser _multiShot;
        [SerializeField] private InsatiableHungerUser _insatiableHunger;
        [SerializeField] private RangeAbilityData _abilityDataFirstLevel;
        [SerializeField] private RangeAbilityData _abilityDataSecondLevel;
        [SerializeField] private RangeAbilityData _abilityDataThirdLevel;
        
        private int _counterForMultiShot = 0;
        private int _counterForInsatiableHunger = 0;
        private int _counterForBlur = 0;
        
        public event Action MultiShotUpgraded;
        public event Action InsatiableHungerUpgraded;
        public event Action BlurUpgraded;
        
        public MultiShotUser MultiShotUser => _multiShot;
        public InsatiableHungerUser InsatiableHunger => _insatiableHunger;
        
        public int CurrentMultiShotLevel => _counterForMultiShot;
        public int CurrentInsatiableHunger => _counterForInsatiableHunger;
        public int CurrentBlurLevel => _counterForBlur;
        
        private void Awake()
        {
            AbilitiesDatas = new Dictionary<int, RangeAbilityData> { { FirstLevel, _abilityDataFirstLevel }, { SecondLevel, _abilityDataSecondLevel }, { ThirdLevel, _abilityDataThirdLevel }, };
            
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
        
        public override void UseFirstAbility()
        {
            StartCoroutine(_multiShot.UseAbility(_player.Damage));
        }
        
        public override void UseSecondAbility()
        {
            StartCoroutine(_insatiableHunger.UseAbility(_player));
        }
        
        public override void UpgradeFirstAbility()
        {
            TryUpgradeAbility(ref _counterForMultiShot, FirstLevel, SecondLevel, ThirdLevel, data => data.MultiShotScriptableObject, _multiShot.Upgrade, MultiShotUpgraded);
        }

        public override void UpgradeSecondAbility()
        {
            TryUpgradeAbility(ref _counterForInsatiableHunger, FirstLevel, SecondLevel, ThirdLevel, data => data.InsatiableHungerScriptableObject, _insatiableHunger.Upgrade, InsatiableHungerUpgraded);
        }

        public override void UpgradeThirdAbility()
        {
            TryUpgradeAbility(ref _counterForBlur, FirstLevel, SecondLevel, ThirdLevel, data => data.BlurScriptableObject, scriptableObject => _player.SetEvasion(scriptableObject), BlurUpgraded);
        }
    }
}