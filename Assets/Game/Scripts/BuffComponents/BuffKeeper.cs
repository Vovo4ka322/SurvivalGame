using UnityEngine;
using Game.Scripts.BuffComponents.BuffVariants;

namespace Game.Scripts.BuffComponents
{
    [CreateAssetMenu(menuName = "BuffKeeper", fileName = "Buff/BuffKeeper")]
    public class BuffKeeper : ScriptableObject
    {
        [field: SerializeField] public ArmorBuff ArmorBuffScriptableObject { get; private set; }
        [field: SerializeField] public HealthBuff HealthBuffScriptableObject { get; private set; }
        [field: SerializeField] public DamageBuff DamageBuffScriptableObject { get; private set; }
        [field: SerializeField] public AttackSpeedBuff AttackSpeedBuffScriptableObject { get; private set; }
        [field: SerializeField] public MovementSpeedBuff MovementSpeedBuffScriptableObject { get; private set; }
    }
}