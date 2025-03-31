using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuffComponents
{
    [CreateAssetMenu(menuName = "BuffKeeper", fileName = "Buff/BuffKeeper")]
    public class BuffKeeper : ScriptableObject
    {
        [SerializeField] private List<Buff> _buffs = new List<Buff>();
        [SerializeField] private int _level;
        
        public int Level => _level;
        
        public Buff GetBuff(BuffType buffType)
        {
            return _buffs.Find(buff => buff.Type == buffType);
        }
    }
}