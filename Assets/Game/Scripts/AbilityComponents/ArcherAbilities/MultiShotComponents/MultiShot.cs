using UnityEngine;
using Game.Scripts.Interfaces;

namespace Game.Scripts.AbilityComponents.ArcherAbilities.MultiShotComponents
{
    [CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/Range/MultiShot")]
    public class MultiShot : CharacterAbility, ICooldownable, IDurationable
    {
        [field: SerializeField] public float CooldownTime { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public int SpreadAngle { get; private set; }
        [field: SerializeField] public int ArrowCount { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
    }
}