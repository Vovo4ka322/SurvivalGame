using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.Scripts.AbilityComponents.AbilityUpgrades;
using Game.Scripts.AbilityComponents.ArcherAbilities;
using Game.Scripts.AbilityComponents.MeleeAbilities;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents
{
    public class AbilityWindowImprovement : MonoBehaviour
    {
        [SerializeField] private Image _abilityPanel;
        [SerializeField] private List<UpgradeFieldConfig> _upgradeConfigs;
        
        private MonoBehaviour _abilityComponent;
        private UpgradeDisplayHelper _upgradeHelper;

        private void Awake()
        {
            _upgradeHelper = new UpgradeDisplayHelper();
        }

        public void Init(Player player)
        {
            _abilityComponent = player.GetComponentInChildren<MeleePlayerAbility>();
            
            if (_abilityComponent == null)
            {
                _abilityComponent = player.GetComponentInChildren<RangePlayerAbility>();
            }
            
            if (_abilityComponent == null)
            {
                return;
            }
            
            SubscribeToAbilityEvents();
        }

        private void OnDisable()
        {
            UnsubscribeAbilityEvents();
        }

        private void SubscribeToAbilityEvents()
        {
            if(_abilityComponent is MeleePlayerAbility melee)
            {
                melee.LevelChanged += PressPlayerAbilityUpgrade;
                melee.BladeFuryUpgraded += ClosePlayerAbilityPanel;
                melee.BorrowedTimeIUpgraded += ClosePlayerAbilityPanel;
                melee.BloodLustIUpgraded += ClosePlayerAbilityPanel;
            }
            else if(_abilityComponent is RangePlayerAbility range)
            {
                range.LevelChanged += PressPlayerAbilityUpgrade;
                range.MultiShotUpgraded += ClosePlayerAbilityPanel;
                range.InsatiableHungerUpgraded += ClosePlayerAbilityPanel;
                range.BlurUpgraded += ClosePlayerAbilityPanel;
            }
        }

        private void UnsubscribeAbilityEvents()
        {
            if(_abilityComponent is MeleePlayerAbility melee)
            {
                melee.LevelChanged -= PressPlayerAbilityUpgrade;
                melee.BladeFuryUpgraded -= ClosePlayerAbilityPanel;
                melee.BorrowedTimeIUpgraded -= ClosePlayerAbilityPanel;
                melee.BloodLustIUpgraded -= ClosePlayerAbilityPanel;
            }
            else if(_abilityComponent is RangePlayerAbility range)
            {
                range.LevelChanged -= PressPlayerAbilityUpgrade;
                range.MultiShotUpgraded -= ClosePlayerAbilityPanel;
                range.InsatiableHungerUpgraded -= ClosePlayerAbilityPanel;
                range.BlurUpgraded -= ClosePlayerAbilityPanel;
            }
        }

        private void PressPlayerAbilityUpgrade()
        {
            UpdateUpgradeTexts();
            
            Time.timeScale = 0f;
            
            _abilityPanel.gameObject.SetActive(true);
        }

        private void ClosePlayerAbilityPanel()
        {
            Time.timeScale = 1f;
            
            _abilityPanel.gameObject.SetActive(false);
        }

        private void UpdateUpgradeTexts()
        {
            if(_abilityComponent is MeleePlayerAbility melee)
            {
                foreach(UpgradeFieldConfig config in _upgradeConfigs)
                {
                    switch(config.NameAbilityType)
                    {
                        case NameAbilityType.BladeFury:
                            
                            foreach(FieldDisplayConfig field in config.FieldDisplays)
                            {
                                switch(field.Property)
                                {
                                    case NameAbilityProperty.Duration:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, melee.CurrentBladeFuryLevel, data => data.BladeFury.Duration, melee.GetAbilityDataForLevel);

                                        break;
                                    case NameAbilityProperty.Cooldown:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, melee.CurrentBladeFuryLevel, data => data.BladeFury.CooldownTime, melee.GetAbilityDataForLevel);

                                        break;
                                }
                            }

                            break;
                        case NameAbilityType.BorrowedTime:
                            
                            foreach(FieldDisplayConfig field in config.FieldDisplays)
                            {
                                switch(field.Property)
                                {
                                    case NameAbilityProperty.Duration:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, melee.CurrentBorrowedTimeLevel, data => data.BorrowedTime.Duration, melee.GetAbilityDataForLevel);

                                        break;
                                    case NameAbilityProperty.Cooldown:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, melee.CurrentBorrowedTimeLevel, data => data.BorrowedTime.CooldownTime, melee.GetAbilityDataForLevel);

                                        break;
                                }
                            }

                            break;
                        case NameAbilityType.BloodLust:
                            
                            foreach(FieldDisplayConfig field in config.FieldDisplays)
                            {
                                switch(field.Property)
                                {
                                    case NameAbilityProperty.MovementSpeed: 
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, melee.CurrentBloodLustLevel, data => data.BloodLust.MovementSpeed, melee.GetAbilityDataForLevel);

                                        break;
                                    case NameAbilityProperty.AttackSpeed:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, melee.CurrentBloodLustLevel, data => data.BloodLust.AttackSpeed, melee.GetAbilityDataForLevel);

                                        break;
                                }
                            }

                            break;
                    }
                }
            }
            else if(_abilityComponent is RangePlayerAbility range)
            {
                foreach(UpgradeFieldConfig config in _upgradeConfigs)
                {
                    switch(config.NameAbilityType)
                    {
                        case NameAbilityType.MultiShot:
                            
                            foreach(FieldDisplayConfig field in config.FieldDisplays)
                            {
                                switch(field.Property)
                                {
                                    case NameAbilityProperty.Duration:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, range.CurrentMultiShotLevel, data => data.MultiShot.Duration, range.GetAbilityDataForLevel);

                                        break;
                                    case NameAbilityProperty.Cooldown:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, range.CurrentMultiShotLevel, data => data.MultiShot.CooldownTime, range.GetAbilityDataForLevel);

                                        break;
                                    case NameAbilityProperty.ArrowCount:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, range.CurrentMultiShotLevel, data => data.MultiShot.ArrowCount, range.GetAbilityDataForLevel);

                                        break;
                                }
                            }

                            break;
                        case NameAbilityType.InsatiableHunger:
                            
                            foreach(FieldDisplayConfig field in config.FieldDisplays)
                            {
                                switch(field.Property)
                                {
                                    case NameAbilityProperty.Duration:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, range.CurrentInsatiableHunger, data => data.InsatiableHunger.Duration, range.GetAbilityDataForLevel);

                                        break;
                                    case NameAbilityProperty.Cooldown:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, range.CurrentInsatiableHunger, data => data.InsatiableHunger.CooldownTime, range.GetAbilityDataForLevel);

                                        break;
                                    case NameAbilityProperty.Vampirism:
                                        _upgradeHelper.UpdateUpgradeDisplay(field.Display, range.CurrentInsatiableHunger, data => data.InsatiableHunger.Vampirism, range.GetAbilityDataForLevel);

                                        break;
                                }
                            }

                            break;
                        case NameAbilityType.Blur:
                            
                            foreach(FieldDisplayConfig field in config.FieldDisplays)
                            {
                                if(field.Property == NameAbilityProperty.Evasion)
                                {
                                    _upgradeHelper.UpdateUpgradeDisplay(field.Display, range.CurrentBlurLevel, data => data.Blur.Evasion, range.GetAbilityDataForLevel);
                                }
                            }

                            break;
                    }
                }
            }
        }
    }
}