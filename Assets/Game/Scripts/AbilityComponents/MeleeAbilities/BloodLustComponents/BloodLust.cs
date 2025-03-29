using UnityEngine;

namespace Game.Scripts.AbilityComponents.MeleeAbilities.BloodLustComponents
{
    [CreateAssetMenu(fileName = "BloodLust", menuName = "Ability/Melee/BloodLust")]
    public class BloodLust : CharacterAbility
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
    }
}