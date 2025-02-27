using UnityEngine;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.Projectiles
{
    public class MagicMovement : IProjectileMovement
    {
        private Vector3 _targetDirection;
        
        public void Launch(BaseProjectile projectile, Vector3 targetPosition)
        {
            Vector3 aimOffset = new Vector3(0, projectile.AimHeight, 0);
            Vector3 direction = (targetPosition + aimOffset) - projectile.transform.position;
            _targetDirection = direction.normalized;
            
            projectile.PlayEffects();
        }
        
        public void Move(BaseProjectile projectile)
        {
            if(_targetDirection != Vector3.zero)
            {
                float deltaSpeed = projectile.Speed * Time.deltaTime;
                projectile.transform.Translate(_targetDirection * deltaSpeed, Space.World);
            }
        }
        
        public void Stop()
        {
            _targetDirection = Vector3.zero;
        }
    }
}