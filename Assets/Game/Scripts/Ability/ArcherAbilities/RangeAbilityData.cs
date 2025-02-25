using UnityEngine;

namespace Ability.ArcherAbilities
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Ability/Range/AbilityData")]
    public class RangeAbilityData : ScriptableObject
    {
        [field:SerializeField] public Multishot.Multishot Multishot {  get; private set; }

        [field: SerializeField] public InsatiableHunger.InsatiableHunger InsatiableHunger { get; private set; }

        [field: SerializeField] public Blur.Blur Blur { get; private set; }
    }
}