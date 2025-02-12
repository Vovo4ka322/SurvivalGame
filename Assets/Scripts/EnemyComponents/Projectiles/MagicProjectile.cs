using UnityEngine;
using Pools;

namespace EnemyComponents.Projectiles
{
    public class MagicProjectile : BaseProjectile
    {
        private readonly IProjectileMovement _movement = new MagicMovement();
        
        public override void Launch(Vector3 targetPosition, ProjectilePool<BaseProjectile> pool)
        {
            LaunchProjectile(_movement, targetPosition, pool);
        }
    }
}