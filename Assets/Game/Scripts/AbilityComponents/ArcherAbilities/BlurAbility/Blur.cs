using UnityEngine;

namespace Game.Scripts.AbilityComponents.ArcherAbilities.BlurAbility
{
    [CreateAssetMenu(fileName = "Blur", menuName = "Ability/Range/Blur")]
    public class Blur : CharacterAbility
    {
        [field: SerializeField] public float Evasion { get; private set; }
    }
}