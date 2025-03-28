using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    public class MeleeAbilityViewer : AbilityViewerBase
    {
        private MeleeAbilityUser _meleeAbilityUser;

        private int _bladeFuryImprovement = 0;
        private int _borrowedTimeImprovement = 0;
        private int _bloodLustImprovement = 0;

        private void OnDisable()
        {
            _meleeAbilityUser.BladeFury.Used -= OnBladeFuryChanged;
            _meleeAbilityUser.BorrowedTime.Used -= OnBorrowedTimeChanged;
            _meleeAbilityUser.BladeFuryUpgraded -= OnBladeFuryUpgraded;
            _meleeAbilityUser.BorrowedTimeIUpgraded -= OnBorrowedTimeUpgraded;
            _meleeAbilityUser.BloodLustIUpgraded -= OnBloodLustUpgraded;
        }

        public void Init(Player player)
        {
            _meleeAbilityUser = player.GetComponentInChildren<MeleeAbilityUser>();
            SetInitialIconsDimmed();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _meleeAbilityUser.BladeFury.Used += OnBladeFuryChanged;
            _meleeAbilityUser.BorrowedTime.Used += OnBorrowedTimeChanged;
            _meleeAbilityUser.BladeFuryUpgraded += OnBladeFuryUpgraded;
            _meleeAbilityUser.BorrowedTimeIUpgraded += OnBorrowedTimeUpgraded;
            _meleeAbilityUser.BloodLustIUpgraded += OnBloodLustUpgraded;
        }

        private void OnBladeFuryChanged(float value)
        {
            Change(FirstAbilityCooldown, _meleeAbilityUser.BladeFury.BladeFury.CooldownTime, value);
        }

        private void OnBorrowedTimeChanged(float value)
        {
            Change(SecondAbilityCooldown, _meleeAbilityUser.BorrowedTime.BorrowedTime.CooldownTime, value);
        }

        private void OnBladeFuryUpgraded()
        {
            Upgrade(FirstAbilityImprovements, ref _bladeFuryImprovement, _meleeAbilityUser.MaxValue, FirstAbility);
        }

        private void OnBorrowedTimeUpgraded()
        {
            Upgrade(SecondAbilityImprovements, ref _borrowedTimeImprovement, _meleeAbilityUser.MaxValue, SecondAbility);
        }

        private void OnBloodLustUpgraded()
        {
            Upgrade(ThirdAbilityImprovements, ref _bloodLustImprovement, _meleeAbilityUser.MaxValue, ThirdAbility);
        }
    }
}