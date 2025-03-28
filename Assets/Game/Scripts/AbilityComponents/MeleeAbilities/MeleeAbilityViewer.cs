using Game.Scripts.MenuComponents;
using Game.Scripts.PlayerComponents;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    public class MeleeAbilityViewer : MonoBehaviour
    {
        [Header("Images Abilities")]
        [SerializeField] private Image _firstAbility;
        [SerializeField] private Image _secondAbility;
        [SerializeField] private Image _thirdAbility;

        [Header("Cooldown Images")]
        [SerializeField] private Image _firstAbilityCooldown;
        [SerializeField] private Image _secondAbilityCooldown;

        [Header("Upgrade Images")]
        [SerializeField] private List<Image> _firstAbilityImprovements;
        [SerializeField] private List<Image> _secondAbilityImprovements;
        [SerializeField] private List<Image> _thirdAbilityImprovements;

        private MeleeAbilityUser _meleeAbilityUser;
        private IconUtility _iconUtility;

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
            _iconUtility = new IconUtility();

            SubscribeToEvents();

            _iconUtility.SetIconDimmed(_firstAbility, true);
            _iconUtility.SetIconDimmed(_secondAbility, true);
            _iconUtility.SetIconDimmed(_thirdAbility, true);
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
            Change(_firstAbilityCooldown, _meleeAbilityUser.BladeFury.BladeFury.CooldownTime, value);
        }

        private void OnBorrowedTimeChanged(float value)
        {
            Change(_secondAbilityCooldown, _meleeAbilityUser.BorrowedTime.BorrowedTime.CooldownTime, value);
        }

        private void Change(Image image, float cooldown, float value)
        {
            image.fillAmount = Mathf.InverseLerp(0, cooldown, value);
        }

        private void Upgrade(List<Image> images, int index)
        {
            images[index].gameObject.SetActive(true);
        }

        private void OnBladeFuryUpgraded()
        {
            if (_bladeFuryImprovement == _meleeAbilityUser.MaxValue)
                return;

            Upgrade(_firstAbilityImprovements, _bladeFuryImprovement);
            _bladeFuryImprovement++;

            if (_bladeFuryImprovement == 1)
            {
                _iconUtility.SetIconDimmed(_firstAbility, false);
            }
        }

        private void OnBorrowedTimeUpgraded()
        {
            if (_borrowedTimeImprovement == _meleeAbilityUser.MaxValue)
                return;

            Upgrade(_secondAbilityImprovements, _borrowedTimeImprovement);
            _borrowedTimeImprovement++;

            if (_borrowedTimeImprovement == 1)
            {
                _iconUtility.SetIconDimmed(_secondAbility, false);
            }
        }

        private void OnBloodLustUpgraded()
        {
            if (_bloodLustImprovement == _meleeAbilityUser.MaxValue)
                return;

            Upgrade(_thirdAbilityImprovements, _bloodLustImprovement);
            _bloodLustImprovement++;

            if (_bloodLustImprovement == 1)
            {
                _iconUtility.SetIconDimmed(_thirdAbility, false);
            }
        }
    }
}