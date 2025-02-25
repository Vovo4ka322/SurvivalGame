using UnityEngine;
using Pools;

namespace EnemyComponents.Projectiles
{
    public class StoneProjectile : BaseProjectile
    {
        private readonly IProjectileMovement _movement = new StoneMovement();
        
        public override void Launch(Vector3 targetPosition, ProjectilePool<BaseProjectile> pool)
        {
            LaunchProjectile(_movement, targetPosition, pool);
        }
    }
}