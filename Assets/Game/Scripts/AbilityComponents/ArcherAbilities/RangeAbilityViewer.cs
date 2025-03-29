using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.ArcherAbilities
{
    public class RangeAbilityViewer : AbilityViewerBase
    {
        private RangePlayerAbility _rangePlayerAbility;
        
        private int _multiShotImprovement = 0;
        private int _insatiableHungerImprovement = 0;
        private int _blurImprovement = 0;
        
        private void OnDisable()
        {
            _rangePlayerAbility.MultiShotUser.Used -= OnMultiShotChanged;
            _rangePlayerAbility.InsatiableHunger.Used -= OnInsatiableHungerChanged;
            _rangePlayerAbility.MultiShotUpgraded -= OnMultiShotUpgraded;
            _rangePlayerAbility.InsatiableHungerUpgraded -= OnInsatiableHungerUpgraded;
            _rangePlayerAbility.BlurUpgraded -= OnBlurUpgraded;
        }
        
        public void Init(Player player)
        {
            _rangePlayerAbility = player.GetComponentInChildren<RangePlayerAbility>();
            SetInitialIconsDimmed();
            SubscribeToEvents();
        }
        
        private void SubscribeToEvents()
        {
            _rangePlayerAbility.MultiShotUser.Used += OnMultiShotChanged;
            _rangePlayerAbility.InsatiableHunger.Used += OnInsatiableHungerChanged;
            _rangePlayerAbility.MultiShotUpgraded += OnMultiShotUpgraded;
            _rangePlayerAbility.InsatiableHungerUpgraded += OnInsatiableHungerUpgraded;
            _rangePlayerAbility.BlurUpgraded += OnBlurUpgraded;
        }
        
        private void OnMultiShotChanged(float value)
        {
            Change(FirstAbilityCooldown, _rangePlayerAbility.MultiShotUser.MultiShot.CooldownTime, value);
        }
        
        private void OnInsatiableHungerChanged(float value)
        {
            Change(SecondAbilityCooldown, _rangePlayerAbility.InsatiableHunger.InsatiableHunger.CooldownTime, value);
        }
        
        private void OnMultiShotUpgraded()
        {
            Upgrade(FirstAbilityImprovements, ref _multiShotImprovement, _rangePlayerAbility.MaxValue, FirstAbility);
        }
        
        private void OnInsatiableHungerUpgraded()
        {
            Upgrade(SecondAbilityImprovements, ref _insatiableHungerImprovement, _rangePlayerAbility.MaxValue, SecondAbility);
        }
        
        private void OnBlurUpgraded()
        {
            Upgrade(ThirdAbilityImprovements, ref _blurImprovement, _rangePlayerAbility.MaxValue, ThirdAbility);
        }
    }
}
