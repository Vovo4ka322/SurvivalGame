using UnityEngine;

namespace Game.Scripts.ProjectileComponents.ProjectileInterfaces
{
    public interface IProjectileMovement
    {
        public void Launch(BaseProjectile projectile, Vector3 targetPosition);
        public void Move(BaseProjectile projectile);
        public void Stop();
    }
}