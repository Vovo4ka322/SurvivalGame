using UnityEngine;

namespace Game.Scripts.BuffComponents
{
    [CreateAssetMenu(menuName = "Buff", fileName = "Buff/NewBuff")]
    public class Buff : ScriptableObject
    {
        [SerializeField] private BuffType _buffType;
        [SerializeField] private float _value;
        
        public BuffType Type => _buffType;
        public float Value => _value;
    }
}