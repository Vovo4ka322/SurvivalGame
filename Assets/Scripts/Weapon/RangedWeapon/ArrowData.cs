using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrow", menuName = "WeaponType/Archer/Arrow")]
public class ArrowData : WeaponData//Доделать тут и СО лука, а также прокинуть все в основные классы
{
    [field: SerializeField] public int ArrowFlightSpeed { get; private set; }

    [field: SerializeField] public float AttackRadius { get; private set; }
}
