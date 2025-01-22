using UnityEngine;

namespace EnemyComponents.Projectiles
{
    public class StoneMovement : IProjectileMovement
    {
        private Vector3 _velocity;
        
        public void Launch(BaseProjectile projectile, Vector3 targetPosition)
        {
            projectile.PlayEffects();
            
            Vector3 aimOffset = new Vector3(0, projectile.AimHeight, 0);
            Vector3 direction = (targetPosition + aimOffset) - projectile.transform.position;
            
            if(direction != Vector3.zero)
            {
                _velocity = direction.normalized * projectile.Speed;
            } 
            else
            {
                _velocity = Vector3.up * projectile.Speed;
            }
        }
        
        public void Move(BaseProjectile projectile)
        {
            projectile.transform.position += _velocity * Time.deltaTime;
        }
    }
}