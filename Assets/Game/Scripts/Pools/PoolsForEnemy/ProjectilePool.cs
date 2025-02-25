using UnityEngine;
using EnemyComponents.Projectiles;

namespace Pools
{
    public class ProjectilePool<T> : BasePool<T> 
        where T : BaseProjectile
    {
        public ProjectilePool(T prefab, PoolSettings settings, Transform container = null) : base(prefab, settings, container)
        {
        }
    }
}