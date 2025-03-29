using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    public class MeleeAbilityViewer : AbilityViewerBase
    {
        private MeleePlayerAbility _meleePlayerAbility;

        private int _bladeFuryImprovement = 0;
        private int _borrowedTimeImprovement = 0;
        private int _bloodLustImprovement = 0;

        private void OnDisable()
        {
            _meleePlayerAbility.BladeFury.Used -= OnBladeFuryChanged;
            _meleePlayerAbility.BorrowedTime.Used -= OnBorrowedTimeChanged;
            _meleePlayerAbility.BladeFuryUpgraded -= OnBladeFuryUpgraded;
            _meleePlayerAbility.BorrowedTimeIUpgraded -= OnBorrowedTimeUpgraded;
            _meleePlayerAbility.BloodLustIUpgraded -= OnBloodLustUpgraded;
        }

        public void Init(Player player)
        {
            _meleePlayerAbility = player.GetComponentInChildren<MeleePlayerAbility>();
            SetInitialIconsDimmed();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _meleePlayerAbility.BladeFury.Used += OnBladeFuryChanged;
            _meleePlayerAbility.BorrowedTime.Used += OnBorrowedTimeChanged;
            _meleePlayerAbility.BladeFuryUpgraded += OnBladeFuryUpgraded;
            _meleePlayerAbility.BorrowedTimeIUpgraded += OnBorrowedTimeUpgraded;
            _meleePlayerAbility.BloodLustIUpgraded += OnBloodLustUpgraded;
        }

        private void OnBladeFuryChanged(float value)
        {
            Change(FirstAbilityCooldown, _meleePlayerAbility.BladeFury.BladeFury.CooldownTime, value);
        }

        private void OnBorrowedTimeChanged(float value)
        {
            Change(SecondAbilityCooldown, _meleePlayerAbility.BorrowedTime.BorrowedTime.CooldownTime, value);
        }

        private void OnBladeFuryUpgraded()
        {
            Upgrade(FirstAbilityImprovements, ref _bladeFuryImprovement, _meleePlayerAbility.MaxValue, FirstAbility);
        }

        private void OnBorrowedTimeUpgraded()
        {
            Upgrade(SecondAbilityImprovements, ref _borrowedTimeImprovement, _meleePlayerAbility.MaxValue, SecondAbility);
        }

        private void OnBloodLustUpgraded()
        {
            Upgrade(ThirdAbilityImprovements, ref _bloodLustImprovement, _meleePlayerAbility.MaxValue, ThirdAbility);
        }
    }
}