using UnityEngine;

[CreateAssetMenu(fileName = "BladeFury", menuName = "Ability/Melee/BladeFury")]
public class BladeFury : Ability, IDamageCausable, IRadiusable, IDurationable, ICooldownable
{
    [field: SerializeField] public float Damage {  get; private set; }

    [field: SerializeField] public float Radius {  get; private set; }

    [field: SerializeField] public float TurnSpeed { get; private set; }

    [field: SerializeField] public float Duration { get; private set; }

    [field: SerializeField] public float CooldownTime { get; private set; }
}