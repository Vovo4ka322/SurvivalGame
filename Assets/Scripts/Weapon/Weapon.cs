using Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [field: SerializeField] public WeaponData WeaponData { get; private set; }
}
