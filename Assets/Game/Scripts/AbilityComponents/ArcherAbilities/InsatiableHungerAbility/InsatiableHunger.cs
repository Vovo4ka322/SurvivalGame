using UnityEngine;
using Game.Scripts.Interfaces;

namespace Game.Scripts.AbilityComponents.ArcherAbilities.InsatiableHungerAbility
{
    [CreateAssetMenu(fileName = "InsatiableHunger", menuName = "Ability/Range/InsatiableHunger")]
    public class InsatiableHunger : CharacterAbility, ICooldownable
    {
        [field: SerializeField] public float CooldownTime { get; private set; }
        [field: SerializeField] public float Vampirism { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}