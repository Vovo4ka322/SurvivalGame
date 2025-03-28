using UnityEngine;

namespace Game.Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
        public float TotalDamage { get; private set; }

        public void SetTotalDamage(float totalDamage) => TotalDamage = totalDamage;
    }
}