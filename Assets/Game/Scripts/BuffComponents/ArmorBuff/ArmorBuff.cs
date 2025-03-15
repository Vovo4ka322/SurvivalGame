using UnityEngine;

[CreateAssetMenu(menuName = "ArmorhBuff", fileName = "Buff/ArmorBuff")]
public class ArmorBuff : ScriptableObject
{
    [field: SerializeField] public float Value { get; private set; }
}