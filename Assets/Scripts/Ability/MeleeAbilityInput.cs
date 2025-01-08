using Ability.MeleeAbilities;
using UnityEngine;
using UnityEngine.UI;

namespace Ability
{
    public class MeleeAbilityInput : MonoBehaviour
    {
        [SerializeField] private MeleeAbilityUser _meleeAbilityUser;
        [SerializeField] private PlayerController _playerController;

        [Header("Subscribe to use buttons")]
        [SerializeField] private Button _firstAbilityUse;
        [SerializeField] private Button _secondAbilityUse;

        [Header("Subscribe to upgrade buttons")]
        [SerializeField] private Button _firstUpgradeButton;
        [SerializeField] private Button _secondUpgradeButton;
        [SerializeField] private Button _thirdUpgradeButton;

        private void Awake()
        {
            _firstAbilityUse.onClick.AddListener(_meleeAbilityUser.UseFirstAbility);
            _secondAbilityUse.onClick.AddListener(_meleeAbilityUser.UseSecondAbility);
            _firstUpgradeButton.onClick.AddListener(_meleeAbilityUser.UpgradeFirstAbility);
            _secondUpgradeButton.onClick.AddListener(_meleeAbilityUser.UpgradeSecondAbility);
            _thirdUpgradeButton.onClick.AddListener(_meleeAbilityUser.UpgradeThirdAbility);
        }

        private void Update()
        {
            if(_playerController.FirstAbilityKeyPressed)
                _meleeAbilityUser.UseFirstAbility();
            else if(_playerController.SecondAbilityKeyPressed)
                _meleeAbilityUser.UseSecondAbility();
        }

        private void OnDisable()
        {
            _firstAbilityUse.onClick.RemoveListener(_meleeAbilityUser.UseFirstAbility);
            _secondAbilityUse.onClick.RemoveListener(_meleeAbilityUser.UseSecondAbility);
            _firstUpgradeButton.onClick.RemoveListener(_meleeAbilityUser.UpgradeFirstAbility);
            _secondUpgradeButton.onClick.RemoveListener(_meleeAbilityUser.UpgradeSecondAbility);
            _thirdUpgradeButton.onClick.RemoveListener(_meleeAbilityUser.UpgradeThirdAbility);
        }
    }
}
