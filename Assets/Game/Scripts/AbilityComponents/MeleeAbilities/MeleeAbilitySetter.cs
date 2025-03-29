using UnityEngine;
using Game.Scripts.AbilityComponents.MeleeAbilities.BladeFuryComponents;
using Game.Scripts.AbilityComponents.MeleeAbilities.BloodLustComponents;
using Game.Scripts.AbilityComponents.MeleeAbilities.BorrowedTimeComponents;

namespace Game.Scripts.AbilityComponents.MeleeAbilities
{
    [CreateAssetMenu(fileName = "MeleeAbilitySetter", menuName = "Ability/Melee/AbilityData")]
    public class MeleeAbilitySetter : ScriptableObject
    {
        [field: SerializeField] public BorrowedTime BorrowedTimeScriptableObject { get; private set; }
        [field: SerializeField] public BladeFury BladeFuryScriptableObject { get; private set; }
        [field: SerializeField] public BloodLust BloodLustScriptableObject { get; private set; }
    }
}