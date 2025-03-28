using Game.Scripts.ProjectileComponents;
using UnityEngine;

namespace Game.Scripts.PoolComponents
{
    public class ProjectilePool<T> : BasePool<T>
        where T : BaseProjectile
    {
        public ProjectilePool(T prefab, PoolSettings settings, Transform container = null)
            : base(prefab, settings, container)
        {
        }
    }
}