using UnityEngine;
using Game.Scripts.Interfaces;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject, IDamageCausable
{
    [field: SerializeField] public WeaponType WeaponType { get; private set; }

    [field: SerializeField] public float Damage { get; private set; }
}