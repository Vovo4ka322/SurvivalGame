using UnityEngine;

[CreateAssetMenu(fileName = "BorrowedTime", menuName = "Ability/Melee/BorrowedTime")]
public class BorrowedTime : Ability, IDurationable, ICooldownable
{
    [field: SerializeField] public float Duration {  get; private set; }

    [field: SerializeField] public float CooldownTime { get; private set; }
}
