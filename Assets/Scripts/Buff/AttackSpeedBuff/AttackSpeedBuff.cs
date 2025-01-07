using UnityEngine;

[CreateAssetMenu(menuName = "AttackSpeedBuff", fileName = "Buff/AttackSpeedBuff")]
public class AttackSpeedBuff : ScriptableObject
{
    [field: SerializeField] public float Value { get; private set; }
}