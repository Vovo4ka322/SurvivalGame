using UnityEngine;

namespace Ability.MeleeAbilities
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Ability/Melee/AbilityData")]
    public class MeleeAbilityData : ScriptableObject
    {
        [field: SerializeField] public BorrowedTime.BorrowedTime BorrowedTimeScriptableObject {  get; private set; }
        [field: SerializeField] public BladeFury.BladeFury BladeFuryScriptableObject { get; private set; }
        [field: SerializeField] public Bloodlust.Bloodlust BloodlustScriptableObject { get; private set; }
    }
}
