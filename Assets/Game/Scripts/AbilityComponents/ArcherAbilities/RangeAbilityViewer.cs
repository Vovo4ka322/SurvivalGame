using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.ArcherAbilities
{
    public class RangeAbilityViewer : AbilityViewerBase
    {
        private ArcherAbilityUser _archerAbilityUser;
        
        private int _multiShotImprovement = 0;
        private int _insatiableHungerImprovement = 0;
        private int _blurImprovement = 0;
        
        private void OnDisable()
        {
            _archerAbilityUser.MultiShotUser.Used -= OnMultiShotChanged;
            _archerAbilityUser.InsatiableHunger.Used -= OnInsatiableHungerChanged;
            _archerAbilityUser.MultiShotUpgraded -= OnMultiShotUpgraded;
            _archerAbilityUser.InsatiableHungerUpgraded -= OnInsatiableHungerUpgraded;
            _archerAbilityUser.BlurUpgraded -= OnBlurUpgraded;
        }
        
        public void Init(Player player)
        {
            _archerAbilityUser = player.GetComponentInChildren<ArcherAbilityUser>();
            SetInitialIconsDimmed();
            SubscribeToEvents();
        }
        
        private void SubscribeToEvents()
        {
            _archerAbilityUser.MultiShotUser.Used += OnMultiShotChanged;
            _archerAbilityUser.InsatiableHunger.Used += OnInsatiableHungerChanged;
            _archerAbilityUser.MultiShotUpgraded += OnMultiShotUpgraded;
            _archerAbilityUser.InsatiableHungerUpgraded += OnInsatiableHungerUpgraded;
            _archerAbilityUser.BlurUpgraded += OnBlurUpgraded;
        }
        
        private void OnMultiShotChanged(float value)
        {
            Change(FirstAbilityCooldown, _archerAbilityUser.MultiShotUser.MultiShot.CooldownTime, value);
        }
        
        private void OnInsatiableHungerChanged(float value)
        {
            Change(SecondAbilityCooldown, _archerAbilityUser.InsatiableHunger.InsatiableHunger.CooldownTime, value);
        }
        
        private void OnMultiShotUpgraded()
        {
            Upgrade(FirstAbilityImprovements, ref _multiShotImprovement, _archerAbilityUser.MaxValue, FirstAbility);
        }
        
        private void OnInsatiableHungerUpgraded()
        {
            Upgrade(SecondAbilityImprovements, ref _insatiableHungerImprovement, _archerAbilityUser.MaxValue, SecondAbility);
        }
        
        private void OnBlurUpgraded()
        {
            Upgrade(ThirdAbilityImprovements, ref _blurImprovement, _archerAbilityUser.MaxValue, ThirdAbility);
        }
    }
}
