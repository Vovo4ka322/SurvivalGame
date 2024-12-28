using UnityEngine;

namespace Ability.ArcherAbilities.Multishot
{
    [CreateAssetMenu(fileName = "Multishot", menuName = "Ability/Range/Multishot")]
    public class Multishot : Ability, ICooldownable, IDurationable
    {
        [field: SerializeField] public float CooldownTime { get; private set; }

        [field: SerializeField] public float Duration { get; private set; }

        [field: SerializeField] public int SpreadAngle { get; private set; }

        [field: SerializeField] public int ArrowCount { get; private set; }

        [field: SerializeField] public float Delay { get; private set; }
    }
}
