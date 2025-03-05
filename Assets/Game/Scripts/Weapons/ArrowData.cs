using UnityEngine;
using Game.Scripts.Interfaces;

[CreateAssetMenu(fileName = "Bow", menuName = "WeaponType/Archer/Bow")]
public class ArrowData : WeaponData
{
    [field: SerializeField] public int ArrowFlightSpeed { get; private set; }

    [field: SerializeField] public float AttackRadius { get; private set; }
}