using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "WeaponType")]
public class WeaponData : ScriptableObject, IWeaponable
{
    [field: SerializeField] public WeaponType WeaponType { get; private set; }

    [field: SerializeField] public float Damage { get; private set; }

    [field: SerializeField] public int AttackSpeed { get; private set; }//¬озможно, должно быть в классе игрока

    [field: SerializeField] public float AttackRadius { get; private set; }
}
