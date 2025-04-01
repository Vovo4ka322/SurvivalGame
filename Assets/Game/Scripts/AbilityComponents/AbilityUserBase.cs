using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.Interfaces;

namespace Game.Scripts.AbilityComponents
{
    public abstract class AbilityUserBase<TAbilityData> : MonoBehaviour, IAbilityUser
    {
        private readonly int _firstLevel = 1;
        private readonly int _secondLevel = 2;
        private readonly int _thirdLevel = 3;
        private readonly int _firstUpgrade = 0;
        private readonly int _secondUpgrade = 1;
        private readonly int _thirdUpgrade = 2;
        
        public Dictionary<int, TAbilityData> AbilitiesDatas;
        
        public event Action LevelChanged;
        
        public int MaxValue { get; private set; } = 3;
        public int FirstLevel => _firstLevel;
        public int SecondLevel => _secondLevel;
        public int ThirdLevel => _thirdLevel;
        
        public TAbilityData GetAbilityDataForLevel(int level)
        {
            if(AbilitiesDatas != null && AbilitiesDatas.ContainsKey(level))
            {
                return AbilitiesDatas[level];
            }
            
            return default;
        }
        
        public void OpenUpgraderWindow()
        {
            LevelChanged?.Invoke();
        }
        
        protected void TryUpgradeAbility<TScriptableObject>(ref int counter, int level1, int level2, int level3, Func<TAbilityData, TScriptableObject> getScriptableObject, Action<TScriptableObject> upgradeAction, Action upgradedEvent)
        {
            if(IsTrue(counter, _firstUpgrade))
                Upgrade(ref counter, level1, getScriptableObject, upgradeAction, upgradedEvent);
            else if(IsTrue(counter, _secondUpgrade))
                Upgrade(ref counter, level2, getScriptableObject, upgradeAction, upgradedEvent);
            else if(IsTrue(counter, _thirdUpgrade))
                Upgrade(ref counter, level3, getScriptableObject, upgradeAction, upgradedEvent);
        }
        
        private void Upgrade<TScriptableObject>(ref int counter, int level, Func<TAbilityData, TScriptableObject> getScriptableObject, Action<TScriptableObject> upgradeAction, Action upgradedEvent)
        {
            if(IsMaxValue(counter))
            {
                return;
            }

            TAbilityData data = GetAbilityDataForLevel(level);
            
            upgradeAction(getScriptableObject(data));
            
            counter++;
            
            upgradedEvent?.Invoke();
        }
        
        public abstract void UseFirstAbility();
        
        public abstract void UseSecondAbility();
        
        public abstract void UpgradeFirstAbility();
        
        public abstract void UpgradeSecondAbility();
        
        public abstract void UpgradeThirdAbility();
        
        private bool IsMaxValue(int value) => value == MaxValue;
        
        private bool IsTrue(int counter, int numberOfUpgrade) => counter == numberOfUpgrade;
    }
}