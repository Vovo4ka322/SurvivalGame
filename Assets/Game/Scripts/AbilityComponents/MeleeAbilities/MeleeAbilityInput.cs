using Game.Scripts.PlayerComponents.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    public class MeleeAbilityInput : MonoBehaviour
    {
        [SerializeField] private MeleeAbilityUser _meleeAbilityUser;
        [SerializeField] private PlayerController _playerController;

        private Button _firstAbilityUse;
        private Button _secondAbilityUse;
        private Button _firstUpgradeButton;
        private Button _secondUpgradeButton;
        private Button _thirdUpgradeButton;

        private void Start()
        {
            _firstAbilityUse.onClick.AddListener(_meleeAbilityUser.UseFirstAbility);
            _secondAbilityUse.onClick.AddListener(_meleeAbilityUser.UseSecondAbility);
            _firstUpgradeButton.onClick.AddListener(_meleeAbilityUser.UpgradeFirstAbility);
            _secondUpgradeButton.onClick.AddListener(_meleeAbilityUser.UpgradeSecondAbility);
            _thirdUpgradeButton.onClick.AddListener(_meleeAbilityUser.UpgradeThirdAbility);
        }

        private void Update()
        {
            if (_playerController.FirstAbilityKeyPressed)
                _meleeAbilityUser.UseFirstAbility();
            else if (_playerController.SecondAbilityKeyPressed)
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

        public void Init(Button firstAbilityUse, Button secondAbilityUse, Button firstUpgradeButton, Button secondUpgradeButton, Button thirdUpgradeButton)
        {
            _firstAbilityUse = firstAbilityUse;
            _secondAbilityUse = secondAbilityUse;
            _firstUpgradeButton = firstUpgradeButton;
            _secondUpgradeButton = secondUpgradeButton;
            _thirdUpgradeButton = thirdUpgradeButton;
        }
    }
}
