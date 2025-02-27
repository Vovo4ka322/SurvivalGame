using UnityEngine;
using Game.Scripts.Interfaces;

namespace Ability.MeleeAbilities.BladeFury
{
    [CreateAssetMenu(fileName = "BladeFury", menuName = "Ability/Melee/BladeFury")]
    public class BladeFury : Ability, IRadiusable, IDurationable, ICooldownable
    {
        [field: SerializeField] public float Radius {  get; private set; }

        [field: SerializeField] public float TurnSpeed { get; private set; }

        [field: SerializeField] public float Duration { get; private set; }

        [field: SerializeField] public float CooldownTime { get; private set; }
    }
}