using UnityEngine;

namespace Game.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "Bow", menuName = "WeaponType/Archer/Bow")]
    public class ArrowData : WeaponData
    {
        [field: SerializeField] public int ArrowFlightSpeed { get; private set; }

        [field: SerializeField] public float AttackRadius { get; private set; }
    }
}