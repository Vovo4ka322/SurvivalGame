using UnityEngine;

namespace Game.Scripts.PlayerComponents.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _playerInput;

        public Vector2 Movement { get; private set; }
        public Vector2 Rotation { get; private set; }
        public bool FirstAbilityKeyPressed { get; private set; }
        public bool SecondAbilityKeyPressed { get; private set; }

        private void Awake()
        {
            _playerInput = new ();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Player.Move.performed += OnMovedPerfomed;
            _playerInput.Player.Look2.performed += OnLookPerfomed;
            _playerInput.Player.Move.canceled += OnMovedPerfomed;
            _playerInput.Player.Look2.canceled += OnLookPerfomed;
            _playerInput.Player.UseFirstAbility.started += OnUsedFirstAbility;
            _playerInput.Player.UseFirstAbility.canceled += OnUsedFirstAbility;
            _playerInput.Player.UseSecondAbility.started += OnUsedSecondAbility;
            _playerInput.Player.UseSecondAbility.canceled += OnUsedSecondAbility;
        }

        private void OnDisable()
        {
            _playerInput.Disable();
            _playerInput.Player.Move.performed -= OnMovedPerfomed;
            _playerInput.Player.Look2.performed -= OnLookPerfomed;
            _playerInput.Player.Move.canceled -= OnMovedPerfomed;
            _playerInput.Player.Look2.canceled -= OnLookPerfomed;
            _playerInput.Player.UseFirstAbility.started -= OnUsedFirstAbility;
            _playerInput.Player.UseFirstAbility.canceled -= OnUsedFirstAbility;
            _playerInput.Player.UseSecondAbility.started -= OnUsedSecondAbility;
            _playerInput.Player.UseSecondAbility.canceled -= OnUsedSecondAbility;
        }

        private void OnLookPerfomed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Rotation = context.ReadValue<Vector2>();
        }

        private void OnMovedPerfomed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
        }

        private void OnUsedFirstAbility(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            FirstAbilityKeyPressed = context.ReadValueAsButton();
        }

        private void OnUsedSecondAbility(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            SecondAbilityKeyPressed = context.ReadValueAsButton();
        }
    }
}
