using UnityEngine;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    public class MeleeWindowImprovement : AbilityWindowBase
    {
        [SerializeField] private UpgradeTextDisplay _bladeFuryDurationDisplay;
        [SerializeField] private UpgradeTextDisplay _bladeFuryCooldownDisplay;
        [SerializeField] private UpgradeTextDisplay _borrowedTimeDurationDisplay;
        [SerializeField] private UpgradeTextDisplay _borrowedTimeCooldownDisplay;
        [SerializeField] private UpgradeTextDisplay _bloodLustMovementSpeedDisplay;
        [SerializeField] private UpgradeTextDisplay _bloodLustAttackSpeedDisplay;
        
        private MeleePlayerAbility _meleePlayerAbility;
        
        public override void Init(Player player)
        {
            _meleePlayerAbility = player.GetComponentInChildren<MeleePlayerAbility>();
            SubscribeToEvents();
        }
        
        protected override void SubscribeToEvents()
        {
            _meleePlayerAbility.LevelChanged += PressPlayerAbilityUpgrade;
            _meleePlayerAbility.BladeFuryUpgraded += ClosePlayerAbilityPanel;
            _meleePlayerAbility.BorrowedTimeIUpgraded += ClosePlayerAbilityPanel;
            _meleePlayerAbility.BloodLustIUpgraded += ClosePlayerAbilityPanel;
        }
        
        protected override void UnsubscribeEvents()
        {
            _meleePlayerAbility.LevelChanged -= PressPlayerAbilityUpgrade;
            _meleePlayerAbility.BladeFuryUpgraded -= ClosePlayerAbilityPanel;
            _meleePlayerAbility.BorrowedTimeIUpgraded -= ClosePlayerAbilityPanel;
            _meleePlayerAbility.BloodLustIUpgraded -= ClosePlayerAbilityPanel;
        }
        
        protected override void UpdateUpgradeTexts()
        {
            int currentBladeFury = _meleePlayerAbility.CurrentBladeFuryLevel;
            
            if(currentBladeFury == 0)
            {
                MeleeAbilitySet nextDataBladeFury = _meleePlayerAbility.GetAbilityDataForLevel(1);
                
                if(nextDataBladeFury != null)
                {
                    _bladeFuryDurationDisplay.SetText(0, nextDataBladeFury.BladeFuryScriptableObject.Duration);
                    _bladeFuryCooldownDisplay.SetText(0, nextDataBladeFury.BladeFuryScriptableObject.CooldownTime);
                }
            }
            else
            {
                MeleeAbilitySet currentDataBladeFury = _meleePlayerAbility.GetAbilityDataForLevel(currentBladeFury);
                MeleeAbilitySet nextDataBladeFury = _meleePlayerAbility.GetAbilityDataForLevel(currentBladeFury + 1);
                
                if(currentDataBladeFury != null)
                {
                    float currentDuration = currentDataBladeFury.BladeFuryScriptableObject.Duration;
                    float? nextDuration = nextDataBladeFury != null ? nextDataBladeFury.BladeFuryScriptableObject.Duration : null;
                    
                    _bladeFuryDurationDisplay.SetText(currentDuration, nextDuration);
                    
                    float currentCooldown = currentDataBladeFury.BladeFuryScriptableObject.CooldownTime;
                    float? nextCooldown = nextDataBladeFury != null ? nextDataBladeFury.BladeFuryScriptableObject.CooldownTime : null;
                    
                    _bladeFuryCooldownDisplay.SetText(currentCooldown, nextCooldown);
                }
            }
            
            int currentBorrowedTime = _meleePlayerAbility.CurrentBorrowedTimeLevel;
            
            if(currentBorrowedTime == 0)
            {
                MeleeAbilitySet nextDataBorrowedTime = _meleePlayerAbility.GetAbilityDataForLevel(1);
                
                if(nextDataBorrowedTime != null)
                {
                    _borrowedTimeDurationDisplay.SetText(0, nextDataBorrowedTime.BorrowedTimeScriptableObject.Duration);
                    _borrowedTimeCooldownDisplay.SetText(0, nextDataBorrowedTime.BorrowedTimeScriptableObject.CooldownTime);
                }
            }
            else
            {
                MeleeAbilitySet currentDataBorrowedTime = _meleePlayerAbility.GetAbilityDataForLevel(currentBorrowedTime);
                MeleeAbilitySet nextDataBorrowedTime = _meleePlayerAbility.GetAbilityDataForLevel(currentBorrowedTime + 1);
                
                if(currentDataBorrowedTime != null)
                {
                    float currentDuration = currentDataBorrowedTime.BorrowedTimeScriptableObject.Duration;
                    float? nextDuration = nextDataBorrowedTime != null ? nextDataBorrowedTime.BorrowedTimeScriptableObject.Duration : null;
                    
                    _borrowedTimeDurationDisplay.SetText(currentDuration, nextDuration);
                    
                    float currentCooldown = currentDataBorrowedTime.BorrowedTimeScriptableObject.CooldownTime;
                    float? nextCooldown = nextDataBorrowedTime != null ? nextDataBorrowedTime.BorrowedTimeScriptableObject.CooldownTime : null;
                    
                    _borrowedTimeCooldownDisplay.SetText(currentCooldown, nextCooldown);
                }
            }
            
            int currentBloodLust = _meleePlayerAbility.CurrentBloodLustLevel;
            
            if(currentBloodLust == 0)
            {
                MeleeAbilitySet nextDataBloodLust = _meleePlayerAbility.GetAbilityDataForLevel(1);
                
                if(nextDataBloodLust != null)
                {
                    _bloodLustMovementSpeedDisplay.SetText(0, nextDataBloodLust.BloodLustScriptableObject.MovementSpeed);
                    _bloodLustAttackSpeedDisplay.SetText(0, nextDataBloodLust.BloodLustScriptableObject.AttackSpeed);
                }
            }
            else
            {
                MeleeAbilitySet currentDataBloodLust = _meleePlayerAbility.GetAbilityDataForLevel(currentBloodLust);
                MeleeAbilitySet nextDataBloodLust = _meleePlayerAbility.GetAbilityDataForLevel(currentBloodLust + 1);
                
                if(currentDataBloodLust != null)
                {
                    float currentMovementSpeed = currentDataBloodLust.BloodLustScriptableObject.MovementSpeed;
                    float? nextMovementSpeed = nextDataBloodLust != null ? nextDataBloodLust.BloodLustScriptableObject.MovementSpeed : null;
                    
                    _bloodLustMovementSpeedDisplay.SetText(currentMovementSpeed, nextMovementSpeed);
                    
                    float currentAttackSpeed = currentDataBloodLust.BloodLustScriptableObject.AttackSpeed;
                    float? nextAttackSpeed = nextDataBloodLust != null ? nextDataBloodLust.BloodLustScriptableObject.AttackSpeed : null;
                    
                    _bloodLustAttackSpeedDisplay.SetText(currentAttackSpeed, nextAttackSpeed);
                }
            }
        }
    }
}