using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    public class MeleeWindowImprovement : MonoBehaviour
    {
        [SerializeField] private Image _abilityPanel;
        [SerializeField] private UpgradeTextDisplay _bladeFuryDurationDisplay;
        [SerializeField] private UpgradeTextDisplay _bladeFuryCooldownDisplay;
        [SerializeField] private UpgradeTextDisplay _borrowedTimeDurationDisplay;
        [SerializeField] private UpgradeTextDisplay _borrowedTimeCooldownDisplay;
        [SerializeField] private UpgradeTextDisplay _bloodLustMovementSpeedDisplay;
        [SerializeField] private UpgradeTextDisplay _bloodLustAttackSpeedDisplay;

        private MeleeAbilityUser _meleeAbilityUser;

        private void OnDisable()
        {
            _meleeAbilityUser.LevelChanged -= PressAbilityUpgrade;
            _meleeAbilityUser.BladeFuryUpgraded -= CloseAbilityPanel;
            _meleeAbilityUser.BorrowedTimeIUpgraded -= CloseAbilityPanel;
            _meleeAbilityUser.BloodLustIUpgraded -= CloseAbilityPanel;
        }

        public void Init(Player player)
        {
            _meleeAbilityUser = player.GetComponentInChildren<MeleeAbilityUser>();

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _meleeAbilityUser.LevelChanged += PressAbilityUpgrade;
            _meleeAbilityUser.BladeFuryUpgraded += CloseAbilityPanel;
            _meleeAbilityUser.BorrowedTimeIUpgraded += CloseAbilityPanel;
            _meleeAbilityUser.BloodLustIUpgraded += CloseAbilityPanel;
        }

        private void PressAbilityUpgrade()
        {
            UpdateUpgradeTexts();
            Time.timeScale = 0f;
            _abilityPanel.gameObject.SetActive(true);
        }

        private void CloseAbilityPanel()
        {
            Time.timeScale = 1f;
            _abilityPanel.gameObject.SetActive(false);
        }

        private void UpdateUpgradeTexts()
        {
            int currentBladeFury = _meleeAbilityUser.CurrentBladeFuryLevel;
            
            if (currentBladeFury == 0)
            {
                MeleeAbilityData nextDataBladeFury = _meleeAbilityUser.GetAbilityDataForLevel(1);
                
                if (nextDataBladeFury != null)
                {
                    _bladeFuryDurationDisplay.SetText(0, nextDataBladeFury.BladeFuryScriptableObject.Duration);
                    _bladeFuryCooldownDisplay.SetText(0, nextDataBladeFury.BladeFuryScriptableObject.CooldownTime);
                }
            }
            else
            {
                MeleeAbilityData currentDataBladeFury = _meleeAbilityUser.GetAbilityDataForLevel(currentBladeFury);
                MeleeAbilityData nextDataBladeFury = _meleeAbilityUser.GetAbilityDataForLevel(currentBladeFury + 1);
                
                if (currentDataBladeFury != null)
                {
                    float currentDuration = currentDataBladeFury.BladeFuryScriptableObject.Duration;
                    float? nextDuration = nextDataBladeFury != null ? nextDataBladeFury.BladeFuryScriptableObject.Duration : null;
                    _bladeFuryDurationDisplay.SetText(currentDuration, nextDuration);

                    float currentCooldown = currentDataBladeFury.BladeFuryScriptableObject.CooldownTime;
                    float? nextCooldown = nextDataBladeFury != null ? nextDataBladeFury.BladeFuryScriptableObject.CooldownTime : null;
                    _bladeFuryCooldownDisplay.SetText(currentCooldown, nextCooldown);
                }
            }

            int currentBorrowedTime = _meleeAbilityUser.CurrentBorrowedTimeLevel;
            
            if (currentBorrowedTime == 0)
            {
                MeleeAbilityData nextDataBorrowedTime = _meleeAbilityUser.GetAbilityDataForLevel(1);
                
                if (nextDataBorrowedTime != null)
                {
                    _borrowedTimeDurationDisplay.SetText(0, nextDataBorrowedTime.BorrowedTimeScriptableObject.Duration);
                    _borrowedTimeCooldownDisplay.SetText(0, nextDataBorrowedTime.BorrowedTimeScriptableObject.CooldownTime);
                }
            }
            else
            {
                MeleeAbilityData currentDataBorrowedTime = _meleeAbilityUser.GetAbilityDataForLevel(currentBorrowedTime);
                MeleeAbilityData nextDataBorrowedTime = _meleeAbilityUser.GetAbilityDataForLevel(currentBorrowedTime + 1);
                
                if (currentDataBorrowedTime != null)
                {
                    float currentDuration = currentDataBorrowedTime.BorrowedTimeScriptableObject.Duration;
                    float? nextDuration = nextDataBorrowedTime != null ? nextDataBorrowedTime.BorrowedTimeScriptableObject.Duration : null;
                    _borrowedTimeDurationDisplay.SetText(currentDuration, nextDuration);

                    float currentCooldown = currentDataBorrowedTime.BorrowedTimeScriptableObject.CooldownTime;
                    float? nextCooldown = nextDataBorrowedTime != null ? nextDataBorrowedTime.BorrowedTimeScriptableObject.CooldownTime : null;
                    _borrowedTimeCooldownDisplay.SetText(currentCooldown, nextCooldown);
                }
            }

            int currentBloodLust = _meleeAbilityUser.CurrentBloodLustLevel;
            
            if (currentBloodLust == 0)
            {
                MeleeAbilityData nextDataBloodLust = _meleeAbilityUser.GetAbilityDataForLevel(1);
                
                if (nextDataBloodLust != null)
                {
                    _bloodLustMovementSpeedDisplay.SetText(0, nextDataBloodLust.BloodLustScriptableObject.MovementSpeed);
                    _bloodLustAttackSpeedDisplay.SetText(0, nextDataBloodLust.BloodLustScriptableObject.AttackSpeed);
                }
            }
            else
            {
                MeleeAbilityData currentDataBloodLust = _meleeAbilityUser.GetAbilityDataForLevel(currentBloodLust);
                MeleeAbilityData nextDataBloodLust = _meleeAbilityUser.GetAbilityDataForLevel(currentBloodLust + 1);
                
                if (currentDataBloodLust != null)
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