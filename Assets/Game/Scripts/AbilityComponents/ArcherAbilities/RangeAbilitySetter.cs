using Game.Scripts.AbilityComponents.ArcherAbilities.BlurAbility;
using Game.Scripts.AbilityComponents.ArcherAbilities.InsatiableHungerAbility;
using Game.Scripts.AbilityComponents.ArcherAbilities.MultiShotAbility;
using UnityEngine;

namespace Game.Scripts.AbilityComponents.ArcherAbilities
{
    [CreateAssetMenu(fileName = "RangeAbilitySetter", menuName = "Ability/Range/AbilityData")]
    public class RangeAbilitySetter : ScriptableObject
    {
        [field: SerializeField] public MultiShot MultiShotScriptableObject { get; private set; }
        [field: SerializeField] public InsatiableHunger InsatiableHungerScriptableObject { get; private set; }
        [field: SerializeField] public Blur BlurScriptableObject { get; private set; }
    }
}