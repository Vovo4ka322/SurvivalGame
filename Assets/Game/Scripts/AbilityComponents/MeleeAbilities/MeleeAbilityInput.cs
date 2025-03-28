using Game.Scripts.PlayerComponents.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    public class MeleeAbilityInput : MonoBehaviour
    {
        [SerializeField] private MeleePlayerAbility _meleePlayerAbility;
        [SerializeField] private PlayerController _playerController;

        private Button _firstAbilityUse;
        private Button _secondAbilityUse;
        private Button _firstUpgradeButton;
        private Button _secondUpgradeButton;
        private Button _thirdUpgradeButton;

        private void Start()
        {
            _firstAbilityUse.onClick.AddListener(_meleePlayerAbility.UseFirstAbility);
            _secondAbilityUse.onClick.AddListener(_meleePlayerAbility.UseSecondAbility);
            _firstUpgradeButton.onClick.AddListener(_meleePlayerAbility.UpgradeFirstAbility);
            _secondUpgradeButton.onClick.AddListener(_meleePlayerAbility.UpgradeSecondAbility);
            _thirdUpgradeButton.onClick.AddListener(_meleePlayerAbility.UpgradeThirdAbility);
        }

        private void Update()
        {
            if (_playerController.FirstAbilityKeyPressed)
                _meleePlayerAbility.UseFirstAbility();
            else if (_playerController.SecondAbilityKeyPressed)
                _meleePlayerAbility.UseSecondAbility();
        }

        private void OnDisable()
        {
            _firstAbilityUse.onClick.RemoveListener(_meleePlayerAbility.UseFirstAbility);
            _secondAbilityUse.onClick.RemoveListener(_meleePlayerAbility.UseSecondAbility);
            _firstUpgradeButton.onClick.RemoveListener(_meleePlayerAbility.UpgradeFirstAbility);
            _secondUpgradeButton.onClick.RemoveListener(_meleePlayerAbility.UpgradeSecondAbility);
            _thirdUpgradeButton.onClick.RemoveListener(_meleePlayerAbility.UpgradeThirdAbility);
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
