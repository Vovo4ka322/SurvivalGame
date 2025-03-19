using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickData : MonoBehaviour
{
    [field: SerializeField] public Joystick MovementJoystick {  get; private set; }

    [field: SerializeField] public Joystick RotationJoystick {  get; private set; }
}