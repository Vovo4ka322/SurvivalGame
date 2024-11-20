using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "Buff/Buff")]
public class Buff : ScriptableObject, ICharacteristicable
{
    [field: SerializeField] public int Power { get; private set; }

    [field: SerializeField] public int Armor { get; private set; }

    [field: SerializeField] public int MaxHealth { get; private set; }
}
