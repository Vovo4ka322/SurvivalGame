using UnityEngine;
using Game.Scripts.PoolComponents;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;

namespace Game.Scripts.ProjectileComponents
{
    public class StoneProjectile : BaseProjectile
    {
        private readonly IProjectileMovement _movement = new StoneMovement();
        
        public override void Launch(Vector3 targetPosition, ProjectilePool<BaseProjectile> pool, IExplosionHandler explosionHandler)
        {
            Pool = pool;
            InitializeProjectile(_movement, pool, explosionHandler, ConfiguredLifetime);
            LaunchProjectile(targetPosition);
        }
    }
}