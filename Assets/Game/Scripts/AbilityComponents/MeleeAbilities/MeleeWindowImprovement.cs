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
                AbilitySet nextDataBladeFury = _meleePlayerAbility.GetAbilityDataForLevel(1);
                
                if(nextDataBladeFury != null)
                {
                    _bladeFuryDurationDisplay.SetText(0, nextDataBladeFury.BladeFury.Duration);
                    _bladeFuryCooldownDisplay.SetText(0, nextDataBladeFury.BladeFury.CooldownTime);
                }
            }
            else
            {
                AbilitySet currentDataBladeFury = _meleePlayerAbility.GetAbilityDataForLevel(currentBladeFury);
                AbilitySet nextDataBladeFury = _meleePlayerAbility.GetAbilityDataForLevel(currentBladeFury + 1);
                
                if(currentDataBladeFury != null)
                {
                    float currentDuration = currentDataBladeFury.BladeFury.Duration;
                    float? nextDuration = nextDataBladeFury != null ? nextDataBladeFury.BladeFury.Duration : null;
                    
                    _bladeFuryDurationDisplay.SetText(currentDuration, nextDuration);
                    
                    float currentCooldown = currentDataBladeFury.BladeFury.CooldownTime;
                    float? nextCooldown = nextDataBladeFury != null ? nextDataBladeFury.BladeFury.CooldownTime : null;
                    
                    _bladeFuryCooldownDisplay.SetText(currentCooldown, nextCooldown);
                }
            }
            
            int currentBorrowedTime = _meleePlayerAbility.CurrentBorrowedTimeLevel;
            
            if(currentBorrowedTime == 0)
            {
                AbilitySet nextDataBorrowedTime = _meleePlayerAbility.GetAbilityDataForLevel(1);
                
                if(nextDataBorrowedTime != null)
                {
                    _borrowedTimeDurationDisplay.SetText(0, nextDataBorrowedTime.BorrowedTime.Duration);
                    _borrowedTimeCooldownDisplay.SetText(0, nextDataBorrowedTime.BorrowedTime.CooldownTime);
                }
            }
            else
            {
                AbilitySet currentDataBorrowedTime = _meleePlayerAbility.GetAbilityDataForLevel(currentBorrowedTime);
                AbilitySet nextDataBorrowedTime = _meleePlayerAbility.GetAbilityDataForLevel(currentBorrowedTime + 1);
                
                if(currentDataBorrowedTime != null)
                {
                    float currentDuration = currentDataBorrowedTime.BorrowedTime.Duration;
                    float? nextDuration = nextDataBorrowedTime != null ? nextDataBorrowedTime.BorrowedTime.Duration : null;
                    
                    _borrowedTimeDurationDisplay.SetText(currentDuration, nextDuration);
                    
                    float currentCooldown = currentDataBorrowedTime.BorrowedTime.CooldownTime;
                    float? nextCooldown = nextDataBorrowedTime != null ? nextDataBorrowedTime.BorrowedTime.CooldownTime : null;
                    
                    _borrowedTimeCooldownDisplay.SetText(currentCooldown, nextCooldown);
                }
            }
            
            int currentBloodLust = _meleePlayerAbility.CurrentBloodLustLevel;
            
            if(currentBloodLust == 0)
            {
                AbilitySet nextDataBloodLust = _meleePlayerAbility.GetAbilityDataForLevel(1);
                
                if(nextDataBloodLust != null)
                {
                    _bloodLustMovementSpeedDisplay.SetText(0, nextDataBloodLust.BloodLust.MovementSpeed);
                    _bloodLustAttackSpeedDisplay.SetText(0, nextDataBloodLust.BloodLust.AttackSpeed);
                }
            }
            else
            {
                AbilitySet currentDataBloodLust = _meleePlayerAbility.GetAbilityDataForLevel(currentBloodLust);
                AbilitySet nextDataBloodLust = _meleePlayerAbility.GetAbilityDataForLevel(currentBloodLust + 1);
                
                if(currentDataBloodLust != null)
                {
                    float currentMovementSpeed = currentDataBloodLust.BloodLust.MovementSpeed;
                    float? nextMovementSpeed = nextDataBloodLust != null ? nextDataBloodLust.BloodLust.MovementSpeed : null;
                    
                    _bloodLustMovementSpeedDisplay.SetText(currentMovementSpeed, nextMovementSpeed);
                    
                    float currentAttackSpeed = currentDataBloodLust.BloodLust.AttackSpeed;
                    float? nextAttackSpeed = nextDataBloodLust != null ? nextDataBloodLust.BloodLust.AttackSpeed : null;
                    
                    _bloodLustAttackSpeedDisplay.SetText(currentAttackSpeed, nextAttackSpeed);
                }
            }
        }
    }
}