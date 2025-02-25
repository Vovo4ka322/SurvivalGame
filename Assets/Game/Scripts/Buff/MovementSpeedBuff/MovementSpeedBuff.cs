using UnityEngine;

[CreateAssetMenu(menuName = "MovementSpeedBuff", fileName = "Buff/MovementSpeedBuff")]
public class MovementSpeedBuff : ScriptableObject
{
    [field: SerializeField] public float Value { get; private set; }
}