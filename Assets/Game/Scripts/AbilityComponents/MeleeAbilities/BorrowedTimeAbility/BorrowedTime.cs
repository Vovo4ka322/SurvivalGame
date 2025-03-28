using UnityEngine;
using Game.Scripts.Interfaces;

namespace Game.Scripts.AbilityComponents.MeleeAbilities.BorrowedTimeAbility
{
    [CreateAssetMenu(fileName = "BorrowedTime", menuName = "Ability/Melee/BorrowedTime")]
    public class BorrowedTime : CharacterAbility, IDurationable, ICooldownable
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float CooldownTime { get; private set; }
    }
}