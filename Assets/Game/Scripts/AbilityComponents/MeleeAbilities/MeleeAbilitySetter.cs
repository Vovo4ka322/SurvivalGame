using Game.Scripts.AbilityComponents.MeleeAbilities.BladeFuryAbility;
using Game.Scripts.AbilityComponents.MeleeAbilities.BloodLustAbility;
using Game.Scripts.AbilityComponents.MeleeAbilities.BorrowedTimeAbility;
using UnityEngine;

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