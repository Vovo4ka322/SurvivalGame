using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
    public class WeaponData : ScriptableObject, IDamageCausable
    {
        [field: SerializeField] public WeaponType WeaponType { get; private set; }

        [field: SerializeField] public float Damage { get; private set; }
    }
}