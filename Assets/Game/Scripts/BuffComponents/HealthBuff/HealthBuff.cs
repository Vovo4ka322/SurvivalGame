using UnityEngine;

[CreateAssetMenu(menuName = "HealthBuff", fileName = "Buff/HealthBuff")]
public class HealthBuff : ScriptableObject
{
    [field: SerializeField] public float Value { get; private set; }
}