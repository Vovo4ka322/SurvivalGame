using UnityEngine.InputSystem;

namespace Game.Scripts.Interfaces
{
    public interface IPlayerActions
    {
        public void OnMove(InputAction.CallbackContext context);
        public void OnLook2(InputAction.CallbackContext context);
        public void OnUseFirstAbility(InputAction.CallbackContext context);
        public void OnUseSecondAbility(InputAction.CallbackContext context);
    }
}