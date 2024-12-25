using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook2(InputAction.CallbackContext context);
        void OnUseFirstAbility(InputAction.CallbackContext context);
        void OnUseSecondAbility(InputAction.CallbackContext context);
    }
}