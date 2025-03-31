using UnityEngine;
using Game.Scripts.AbilityComponents.ArcherAbilities.BlurComponents;
using Game.Scripts.AbilityComponents.ArcherAbilities.InsatiableHungerComponents;
using Game.Scripts.AbilityComponents.ArcherAbilities.MultiShotComponents;

namespace Game.Scripts.AbilityComponents.ArcherAbilities
{
    [CreateAssetMenu(fileName = "RangeAbilitySetter", menuName = "Ability/Range/AbilityData")]
    public class RangeAbilitySet : ScriptableObject
    {
        [field: SerializeField] public MultiShot MultiShotScriptableObject { get; private set; }
        [field: SerializeField] public InsatiableHunger InsatiableHungerScriptableObject { get; private set; }
        [field: SerializeField] public Blur BlurScriptableObject { get; private set; }
    }
}