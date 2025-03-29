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
        
        private RangePlayerAbility _rangePlayerAbility;
        
        public override void Init(Player player)
        {
            _rangePlayerAbility = player.GetComponentInChildren<RangePlayerAbility>();
            SubscribeToEvents();
        }
        
        protected override void SubscribeToEvents()
        {
            _rangePlayerAbility.LevelChanged += PressPlayerAbilityUpgrade;
            _rangePlayerAbility.MultiShotUpgraded += ClosePlayerAbilityPanel;
            _rangePlayerAbility.InsatiableHungerUpgraded += ClosePlayerAbilityPanel;
            _rangePlayerAbility.BlurUpgraded += ClosePlayerAbilityPanel;
        }
        
        protected override void UnsubscribeEvents()
        {
            _rangePlayerAbility.LevelChanged -= PressPlayerAbilityUpgrade;
            _rangePlayerAbility.MultiShotUpgraded -= ClosePlayerAbilityPanel;
            _rangePlayerAbility.InsatiableHungerUpgraded -= ClosePlayerAbilityPanel;
            _rangePlayerAbility.BlurUpgraded -= ClosePlayerAbilityPanel;
        }
        
        protected override void UpdateUpgradeTexts()
        {
            int currentMultiShot = _rangePlayerAbility.CurrentMultiShotLevel;
            
            if(currentMultiShot == 0)
            {
                RangeAbilitySetter nextDataMultiShot = _rangePlayerAbility.GetAbilityDataForLevel(1);
                
                if(nextDataMultiShot != null)
                {
                    _multiShotDurationDisplay.SetText(0, nextDataMultiShot.MultiShotScriptableObject.Duration);
                    _multiShotCooldownDisplay.SetText(0, nextDataMultiShot.MultiShotScriptableObject.CooldownTime);
                    _multiShotArrowCountDisplay.SetText(0, nextDataMultiShot.MultiShotScriptableObject.ArrowCount);
                }
            }
            else
            {
                RangeAbilitySetter currentDataMultiShot = _rangePlayerAbility.GetAbilityDataForLevel(currentMultiShot);
                RangeAbilitySetter nextDataMultiShot = _rangePlayerAbility.GetAbilityDataForLevel(currentMultiShot + 1);
                
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
            
            int currentInsatiableHunger = _rangePlayerAbility.CurrentInsatiableHunger;
            
            if(currentInsatiableHunger == 0)
            {
                RangeAbilitySetter nextDataInsatiableHunger = _rangePlayerAbility.GetAbilityDataForLevel(1);
                
                if(nextDataInsatiableHunger != null)
                {
                    _insatiableHungerDurationDisplay.SetText(0, nextDataInsatiableHunger.InsatiableHungerScriptableObject.Duration);
                    _insatiableHungerCooldownDisplay.SetText(0, nextDataInsatiableHunger.InsatiableHungerScriptableObject.CooldownTime);
                    _insatiableHungerVampirismDisplay.SetText(0, nextDataInsatiableHunger.InsatiableHungerScriptableObject.Vampirism);
                }
            }
            else
            {
                RangeAbilitySetter currentDataInsatiableHunger = _rangePlayerAbility.GetAbilityDataForLevel(currentInsatiableHunger);
                RangeAbilitySetter nextDataInsatiableHunger = _rangePlayerAbility.GetAbilityDataForLevel(currentInsatiableHunger + 1);
                
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
            
            int currentBlur = _rangePlayerAbility.CurrentBlurLevel;
            
            if(currentBlur == 0)
            {
                RangeAbilitySetter nextDataBlur = _rangePlayerAbility.GetAbilityDataForLevel(1);
                
                if(nextDataBlur != null)
                {
                    _blurChanceEvasionDisplay.SetText(0, nextDataBlur.BlurScriptableObject.Evasion);
                }
            }
            else
            {
                RangeAbilitySetter currentDataBlur = _rangePlayerAbility.GetAbilityDataForLevel(currentBlur);
                RangeAbilitySetter nextDataBlur = _rangePlayerAbility.GetAbilityDataForLevel(currentBlur + 1);
                
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