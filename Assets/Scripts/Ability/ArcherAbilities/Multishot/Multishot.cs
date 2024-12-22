using UnityEngine;

[CreateAssetMenu(fileName = "Multishot", menuName = "Ability/Range/Multishot")]
public class Multishot : Ability, ICooldownable, IDurationable
{
    [field: SerializeField] public float CooldownTime { get; private set; }

    [field: SerializeField] public float Duration { get; private set; }
}
