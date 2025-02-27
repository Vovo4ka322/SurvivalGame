using UnityEngine;

namespace Game.Scripts.PoolComponents
{
    public class ParticleSystemPool<T> : BasePool<T>
        where T : Component
    {
        public ParticleSystemPool(T prefab, PoolSettings settings, Transform container = null) : base(prefab, settings, container)
        {
        }
    }
}