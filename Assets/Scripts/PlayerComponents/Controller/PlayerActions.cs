using Interfaces;
using UnityEngine.InputSystem;
namespace PlayerComponents.Controller
{
    public readonly struct PlayerActions
    {
        private readonly PlayerInput _mWrapper;
        public PlayerActions(PlayerInput wrapper) { _mWrapper = wrapper; }

        public InputAction Move => _mWrapper.MPlayerMove;
        public InputAction Look2 => _mWrapper.MPlayerLook2;
        public InputAction UseFirstAbility => _mWrapper.MPlayerUseFirstAbility;
        public InputAction UseSecondAbility => _mWrapper.MPlayerUseSecondAbility;

        private InputActionMap Get() => _mWrapper.MPlayer;

        public bool Enabled => Get().enabled;

        public static implicit operator InputActionMap(PlayerActions set) => set.Get();

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (_mWrapper.MPlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in _mWrapper.MPlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);

            _mWrapper.MPlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
        
        private void Enable() => Get().Enable();
        
        private void Disable() => Get().Disable();
        private void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || _mWrapper.MPlayerActionsCallbackInterfaces.Contains(instance)) return;
            _mWrapper.MPlayerActionsCallbackInterfaces.Add(instance);

            Move.started += instance.OnMove;
            Move.performed += instance.OnMove;
            Move.canceled += instance.OnMove;

            Look2.started += instance.OnLook2;
            Look2.performed += instance.OnLook2;
            Look2.canceled += instance.OnLook2;

            UseFirstAbility.started += instance.OnUseFirstAbility;
            UseFirstAbility.performed += instance.OnUseFirstAbility;
            UseFirstAbility.canceled += instance.OnUseFirstAbility;

            UseSecondAbility.started += instance.OnUseSecondAbility;
            UseSecondAbility.performed += instance.OnUseSecondAbility;
            UseSecondAbility.canceled += instance.OnUseSecondAbility;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            Move.started -= instance.OnMove;
            Move.performed -= instance.OnMove;
            Move.canceled -= instance.OnMove;

            Look2.started -= instance.OnLook2;
            Look2.performed -= instance.OnLook2;
            Look2.canceled -= instance.OnLook2;

            UseFirstAbility.started -= instance.OnUseFirstAbility;
            UseFirstAbility.performed -= instance.OnUseFirstAbility;
            UseFirstAbility.canceled -= instance.OnUseFirstAbility;

            UseSecondAbility.started -= instance.OnUseSecondAbility;
            UseSecondAbility.performed -= instance.OnUseSecondAbility;
            UseSecondAbility.canceled -= instance.OnUseSecondAbility;
        }
    }
}