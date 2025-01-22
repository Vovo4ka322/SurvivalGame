using UnityEngine;

namespace  Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
    }
}