using Game.Scripts.ProjectileComponents;
using UnityEngine;

namespace  Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public WeaponData WeaponData { get; private set; }

        [field: SerializeField] public float TotalDamage { get; private set; } 

        public void SetTotalDamage(float totalDamage) => TotalDamage = totalDamage;
    }
} 