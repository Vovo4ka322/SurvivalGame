using UnityEngine;

namespace Weapons.MeleeWeapon
{
    public class Sword : Weapon
    {
        [field: SerializeField] public MeshCollider MeshCollider {  get; private set; }
    }
}