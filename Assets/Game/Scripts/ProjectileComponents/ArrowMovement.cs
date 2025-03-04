using Game.Scripts.ProjectileComponents.ProjectileInterfaces;
using UnityEngine;

namespace Game.Scripts.ProjectileComponents
{
    public class ArrowMovement : IProjectileMovement
    {
        private Vector3 _direction;

        public void Launch(BaseProjectile projectile, Vector3 targetPosition)
        {
            _direction = (targetPosition - projectile.transform.position).normalized;
            projectile.PlayEffects();
        }

        public void Move(BaseProjectile projectile)
        {
            if (_direction != Vector3.zero)
            {
                float delta = projectile.Speed * Time.deltaTime;
                
                projectile.transform.Translate(_direction * delta, Space.World);
            }
        }

        public void Stop()
        {
            _direction = Vector3.zero;
        }
    }
}