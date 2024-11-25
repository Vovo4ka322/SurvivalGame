using UnityEngine;

[CreateAssetMenu(fileName = "BorrowedTime", menuName = "Ability/Melee/BorrowedTime")]
public class BorrowedTime : Ability, IDurationable, IPushable
{
    [field: SerializeField] public float Duration {  get; private set; }

    [field: SerializeField] public KeyCode KeyCode { get; private set; }
}
