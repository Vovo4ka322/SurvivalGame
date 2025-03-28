using Game.Scripts.PoolComponents;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;
using UnityEngine;

namespace Game.Scripts.ProjectileComponents
{
    public class StoneProjectile : BaseProjectile
    {
        public override void Launch(Vector3 targetPosition, BasePool<BaseProjectile> pool, IExplosionHandler explosionHandler)
        {
            IProjectileMovement movement = new StoneMovement();
            Pool = pool;
            InitializeProjectile(movement, pool, explosionHandler, ConfiguredLifetime);
            LaunchProjectile(targetPosition);
        }
    }
}