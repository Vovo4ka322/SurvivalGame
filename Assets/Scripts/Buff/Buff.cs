using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "Buff/Buff")]
public class Buff : ScriptableObject, ICharacteristicable//для улучшения скинов
{
    [field: SerializeField] public int Damage { get; private set; }

    [field: SerializeField] public int Armor { get; private set; }

    [field: SerializeField] public int MaxHealth { get; private set; }
}
