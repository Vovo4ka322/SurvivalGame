using System;
using UnityEngine;

namespace Game.Scripts.PoolComponents
{
    [Serializable]
    public class PoolSettings
    {
        [SerializeField] private bool _collectionCheck = false;
        [SerializeField][Min(0)] private int _initialCapacity = 10;
        [SerializeField][Min(0)] private int _maxPoolSize = 100;
        
        public PoolSettings(int initialCapacity, int maxPoolSize, bool collectionCheck)
        {
            _initialCapacity = initialCapacity;
            _maxPoolSize = maxPoolSize;
            _collectionCheck = collectionCheck;
        }
        
        public int InitialCapacity => _initialCapacity;
        public int MaxPoolSize => _maxPoolSize;
        public bool CollectionCheck => _collectionCheck;
    }
}