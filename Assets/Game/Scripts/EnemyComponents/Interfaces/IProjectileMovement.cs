using UnityEngine;
using Game.Scripts.EnemyComponents.Projectiles;

namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface IProjectileMovement
    {
        public void Launch(BaseProjectile projectile, Vector3 targetPosition);
        public void Move(BaseProjectile projectile);
        public void Stop();
    }
}