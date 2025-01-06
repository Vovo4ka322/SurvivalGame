using UnityEngine;

[CreateAssetMenu(menuName = "DamageBuff", fileName = "Buff/DamageBuff")]
public class DamageBuff : ScriptableObject
{
    [field: SerializeField] public float Value { get; private set; }
}