using UnityEngine;

[CreateAssetMenu(fileName = "BladeFury", menuName = "Ability/Melee/BladeFury")]
public class BladeFury : Ability, IPushable, IDamageCausable, IRadiusable, IDurationable
{
    [field: SerializeField] public KeyCode KeyCode {  get; private set; }

    [field: SerializeField] public float Damage {  get; private set; }

    [field: SerializeField] public float Radius {  get; private set; }

    [field: SerializeField] public float TurnSpeed { get; private set; }

    [field: SerializeField] public float Duration { get; private set; }
}