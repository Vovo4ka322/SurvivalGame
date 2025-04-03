using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents.Controller;

namespace Game.Scripts.AbilityComponents
{
    public class AbilityInput : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _abilityUserMono;
        [SerializeField] private PlayerController _playerController;

        private IAbilityUser _abilityUser;

        private Button _firstAbilityUse;
        private Button _secondAbilityUse;
        private Button _firstUpgradeButton;
        private Button _secondUpgradeButton;
        private Button _thirdUpgradeButton;

        private void Awake()
        {
            _abilityUser = _abilityUserMono as IAbilityUser;
        }

        protected virtual void OnDisable()
        {
            RemoveListeners();
        }

        protected virtual void Start()
        {
            SetupListeners();
        }

        protected virtual void Update()
        {
            if (_playerController.FirstAbilityKeyPressed)
                _abilityUser?.UseFirstAbility();
            else if (_playerController.SecondAbilityKeyPressed)
                _abilityUser?.UseSecondAbility();
        }

        public void Init(Button firstAbilityUse, Button secondAbilityUse, Button firstUpgradeButton,
            Button secondUpgradeButton, Button thirdUpgradeButton)
        {
            _firstAbilityUse = firstAbilityUse;
            _secondAbilityUse = secondAbilityUse;
            _firstUpgradeButton = firstUpgradeButton;
            _secondUpgradeButton = secondUpgradeButton;
            _thirdUpgradeButton = thirdUpgradeButton;
        }

        private void SetupListeners()
        {
            _firstAbilityUse.onClick.AddListener(_abilityUser.UseFirstAbility);
            _secondAbilityUse.onClick.AddListener(_abilityUser.UseSecondAbility);
            _firstUpgradeButton.onClick.AddListener(_abilityUser.UpgradeFirstAbility);
            _secondUpgradeButton.onClick.AddListener(_abilityUser.UpgradeSecondAbility);
            _thirdUpgradeButton.onClick.AddListener(_abilityUser.UpgradeThirdAbility);
        }

        private void RemoveListeners()
        {
            _firstAbilityUse.onClick.RemoveListener(_abilityUser.UseFirstAbility);
            _secondAbilityUse.onClick.RemoveListener(_abilityUser.UseSecondAbility);
            _firstUpgradeButton.onClick.RemoveListener(_abilityUser.UpgradeFirstAbility);
            _secondUpgradeButton.onClick.RemoveListener(_abilityUser.UpgradeSecondAbility);
            _thirdUpgradeButton.onClick.RemoveListener(_abilityUser.UpgradeThirdAbility);
        }
    }
}