using System;
using UnityEngine;

namespace Game.Scripts.AbilityComponents.AbilityUpgrades
{
    [Serializable]
    public class FieldDisplayConfig
    {
        [SerializeField] private NameAbilityProperty _property;
        [SerializeField] private UpgradeTextDisplay _display;
        
        public NameAbilityProperty Property => _property;
        public UpgradeTextDisplay Display => _display;
    }
}