using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.AbilityComponents.AbilityUpgrades
{
    [Serializable]
    public class UpgradeFieldConfig
    {
        [SerializeField] private NameAbilityType _nameAbilityType;
        [SerializeField] private List<FieldDisplayConfig> _fieldDisplays;
        
        public NameAbilityType NameAbilityType => _nameAbilityType;
        public List<FieldDisplayConfig> FieldDisplays => _fieldDisplays;
    }
}