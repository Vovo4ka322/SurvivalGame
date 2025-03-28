using UnityEngine;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.ArcherAbilities
{
    public class RangeWindowImprovement : AbilityWindowBase
    {
        [SerializeField] private UpgradeTextDisplay _multiShotDurationDisplay;
        [SerializeField] private UpgradeTextDisplay _multiShotCooldownDisplay;
        [SerializeField] private UpgradeTextDisplay _multiShotArrowCountDisplay;
        [SerializeField] private UpgradeTextDisplay _insatiableHungerDurationDisplay;
        [SerializeField] private UpgradeTextDisplay _insatiableHungerCooldownDisplay;
        [SerializeField] private UpgradeTextDisplay _insatiableHungerVampirismDisplay;
        [SerializeField] private UpgradeTextDisplay _blurChanceEvasionDisplay;
        
        private ArcherAbilityUser _archerAbilityUser;
        
        public override void Init(Player player)
        {
            _archerAbilityUser = player.GetComponentInChildren<ArcherAbilityUser>();
            SubscribeToEvents();
        }
        
        protected override void SubscribeToEvents()
        {
            _archerAbilityUser.LevelChanged += PressAbilityUpgrade;
            _archerAbilityUser.MultiShotUpgraded += CloseAbilityPanel;
            _archerAbilityUser.InsatiableHungerUpgraded += CloseAbilityPanel;
            _archerAbilityUser.BlurUpgraded += CloseAbilityPanel;
        }
        
        protected override void UnsubscribeEvents()
        {
            _archerAbilityUser.LevelChanged -= PressAbilityUpgrade;
            _archerAbilityUser.MultiShotUpgraded -= CloseAbilityPanel;
            _archerAbilityUser.InsatiableHungerUpgraded -= CloseAbilityPanel;
            _archerAbilityUser.BlurUpgraded -= CloseAbilityPanel;
        }
        
        protected override void UpdateUpgradeTexts()
        {
            int currentMultiShot = _archerAbilityUser.CurrentMultiShotLevel;
            
            if(currentMultiShot == 0)
            {
                RangeAbilityData nextDataMultiShot = _archerAbilityUser.GetAbilityDataForLevel(1);
                
                if(nextDataMultiShot != null)
                {
                    _multiShotDurationDisplay.SetText(0, nextDataMultiShot.MultiShotScriptableObject.Duration);
                    _multiShotCooldownDisplay.SetText(0, nextDataMultiShot.MultiShotScriptableObject.CooldownTime);
                    _multiShotArrowCountDisplay.SetText(0, nextDataMultiShot.MultiShotScriptableObject.ArrowCount);
                }
            }
            else
            {
                RangeAbilityData currentDataMultiShot = _archerAbilityUser.GetAbilityDataForLevel(currentMultiShot);
                RangeAbilityData nextDataMultiShot = _archerAbilityUser.GetAbilityDataForLevel(currentMultiShot + 1);
                
                if(currentDataMultiShot != null)
                {
                    float currentDuration = currentDataMultiShot.MultiShotScriptableObject.Duration;
                    float? nextDuration = nextDataMultiShot != null ? nextDataMultiShot.MultiShotScriptableObject.Duration : null;
                    
                    _multiShotDurationDisplay.SetText(currentDuration, nextDuration);
                    
                    float currentCooldown = currentDataMultiShot.MultiShotScriptableObject.CooldownTime;
                    float? nextCooldown = nextDataMultiShot != null ? nextDataMultiShot.MultiShotScriptableObject.CooldownTime : null;
                    
                    _multiShotCooldownDisplay.SetText(currentCooldown, nextCooldown);
                    
                    float currentArrowCount = currentDataMultiShot.MultiShotScriptableObject.ArrowCount;
                    float? nextArrowCount = nextDataMultiShot != null ? nextDataMultiShot.MultiShotScriptableObject.ArrowCount : null;
                    
                    _multiShotArrowCountDisplay.SetText(currentArrowCount, nextArrowCount);
                }
            }
            
            int currentInsatiableHunger = _archerAbilityUser.CurrentInsatiableHunger;
            
            if(currentInsatiableHunger == 0)
            {
                RangeAbilityData nextDataInsatiableHunger = _archerAbilityUser.GetAbilityDataForLevel(1);
                
                if(nextDataInsatiableHunger != null)
                {
                    _insatiableHungerDurationDisplay.SetText(0, nextDataInsatiableHunger.InsatiableHungerScriptableObject.Duration);
                    _insatiableHungerCooldownDisplay.SetText(0, nextDataInsatiableHunger.InsatiableHungerScriptableObject.CooldownTime);
                    _insatiableHungerVampirismDisplay.SetText(0, nextDataInsatiableHunger.InsatiableHungerScriptableObject.Vampirism);
                }
            }
            else
            {
                RangeAbilityData currentDataInsatiableHunger = _archerAbilityUser.GetAbilityDataForLevel(currentInsatiableHunger);
                RangeAbilityData nextDataInsatiableHunger = _archerAbilityUser.GetAbilityDataForLevel(currentInsatiableHunger + 1);
                
                if(currentDataInsatiableHunger != null)
                {
                    float currentDuration = currentDataInsatiableHunger.InsatiableHungerScriptableObject.Duration;
                    float? nextDuration = nextDataInsatiableHunger != null ? nextDataInsatiableHunger.InsatiableHungerScriptableObject.Duration : null;
                    
                    _insatiableHungerDurationDisplay.SetText(currentDuration, nextDuration);
                    
                    float currentCooldown = currentDataInsatiableHunger.InsatiableHungerScriptableObject.CooldownTime;
                    float? nextCooldown = nextDataInsatiableHunger != null ? nextDataInsatiableHunger.InsatiableHungerScriptableObject.CooldownTime : null;
                    
                    _insatiableHungerCooldownDisplay.SetText(currentCooldown, nextCooldown);
                    
                    float currentVampirism = currentDataInsatiableHunger.InsatiableHungerScriptableObject.Vampirism;
                    float? nextVampirism = nextDataInsatiableHunger != null ? nextDataInsatiableHunger.InsatiableHungerScriptableObject.Vampirism : null;
                    
                    _insatiableHungerVampirismDisplay.SetText(currentVampirism, nextVampirism);
                }
            }
            
            int currentBlur = _archerAbilityUser.CurrentBlurLevel;
            
            if(currentBlur == 0)
            {
                RangeAbilityData nextDataBlur = _archerAbilityUser.GetAbilityDataForLevel(1);
                
                if(nextDataBlur != null)
                {
                    _blurChanceEvasionDisplay.SetText(0, nextDataBlur.BlurScriptableObject.Evasion);
                }
            }
            else
            {
                RangeAbilityData currentDataBlur = _archerAbilityUser.GetAbilityDataForLevel(currentBlur);
                RangeAbilityData nextDataBlur = _archerAbilityUser.GetAbilityDataForLevel(currentBlur + 1);
                
                if(currentDataBlur != null)
                {
                    float currentEvasion = currentDataBlur.BlurScriptableObject.Evasion;
                    float? nextEvasion = nextDataBlur != null ? nextDataBlur.BlurScriptableObject.Evasion : null;
                    
                    _blurChanceEvasionDisplay.SetText(currentEvasion, nextEvasion);
                }
            }
        }
    }
}