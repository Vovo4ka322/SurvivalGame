using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Ability/Range/AbilityData")]
public class RangeAbilityData : ScriptableObject
{
    [field:SerializeField] public Multishot Multishot {  get; private set; }

    [field: SerializeField] public InsatiableHunger InsatiableHunger { get; private set; }

    [field: SerializeField] public Blur Blur { get; private set; }
}