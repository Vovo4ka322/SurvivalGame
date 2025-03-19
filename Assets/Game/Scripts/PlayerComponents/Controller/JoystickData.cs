using UnityEngine;

namespace Game.Scripts.PlayerComponents.Controller
{
    public class JoystickData : MonoBehaviour
    {
        [field: SerializeField] public Joystick MovementJoystick {  get; private set; }
        [field: SerializeField] public Joystick RotationJoystick {  get; private set; }
    }
}