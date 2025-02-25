using UnityEngine;
using UnityEngine.UI;

namespace Ability.ArcherAbilities.Multishot
{
    [CreateAssetMenu(fileName = "Multishot", menuName = "Ability/Range/Multishot")]
    public class Multishot : Ability, ICooldownable, IDurationable, IImagable
    {
        [field: SerializeField] public Image Image { get; private set; }

        [field: SerializeField] public float CooldownTime { get; private set; }

        [field: SerializeField] public float Duration { get; private set; }

        [field: SerializeField] public int SpreadAngle { get; private set; }

        [field: SerializeField] public int ArrowCount { get; private set; }

        [field: SerializeField] public float Delay { get; private set; }
    }
}
