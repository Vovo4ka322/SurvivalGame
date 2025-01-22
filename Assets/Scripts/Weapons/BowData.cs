using UnityEngine;

[CreateAssetMenu(fileName = "Bow", menuName = "WeaponType/Archer/Bow")]
public class BowData : WeaponData, ISpeedable
{
    [field: SerializeField] public float AttackSpeed {  get; private set; }

    [field: SerializeField] public int ArrowFlightSpeed { get; private set; }

    [field: SerializeField] public float AttackRadius { get; private set; }
}