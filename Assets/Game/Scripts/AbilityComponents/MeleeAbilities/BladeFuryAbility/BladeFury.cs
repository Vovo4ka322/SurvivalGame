using UnityEngine;
using Game.Scripts.Interfaces;

namespace Game.Scripts.AbilityComponents.MeleeAbilities.BladeFuryAbility
{
    [CreateAssetMenu(fileName = "BladeFury", menuName = "Ability/Melee/BladeFury")]
    public class BladeFury : CharacterAbility, IDurationable, ICooldownable
    {
        [field: SerializeField] public float TurnSpeed { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float CooldownTime { get; private set; }
    }
}