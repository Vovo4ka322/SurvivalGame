using UnityEngine;
using UnityEngine.UI;

namespace Ability.ArcherAbilities.InsatiableHunger
{
    [CreateAssetMenu(fileName = "InsatiableHunger", menuName = "Ability/Range/InsatiableHunger")]
    public class InsatiableHunger : Ability, ICooldownable, IImagable
    {
        [field: SerializeField] public Image Image { get; private set; }

        [field: SerializeField] public float CooldownTime {  get; private set; }

        [field: SerializeField] public float Vampirism { get; private set; }

        [field: SerializeField] public float Duration { get; private set; }
    }
}
