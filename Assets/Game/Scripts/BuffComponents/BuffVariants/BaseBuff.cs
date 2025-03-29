using UnityEngine;

namespace Game.Scripts.BuffComponents.BuffVariants
{
    public abstract class BaseBuff : ScriptableObject
    {
        [field: SerializeField] public float Value { get; private set; }
    }
}