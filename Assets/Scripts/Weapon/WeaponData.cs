using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "WeaponType")]
public class WeaponData : ScriptableObject
{
    [field: SerializeField] public WeaponType WeaponType { get; private set; }

    [field: SerializeField] public int Damage { get; private set; }

    [field: SerializeField] public int AttackSpeed { get; private set; }

    [field: SerializeField] public float AttackRadius { get; private set; }
}
