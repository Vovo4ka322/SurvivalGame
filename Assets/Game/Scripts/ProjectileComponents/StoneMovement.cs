using UnityEngine;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;

namespace Game.Scripts.ProjectileComponents
{
    public class StoneMovement : IProjectileMovement
    {
        private Vector3 _targetDirection;

        public void Launch(BaseProjectile projectile, Vector3 targetPosition)
        {
            Vector3 aimOffset = new Vector3(0, projectile.AimHeight, 0);
            Vector3 direction = (targetPosition + aimOffset) - projectile.transform.position;

            if (direction != Vector3.zero)
            {
                _targetDirection = direction.normalized * projectile.Speed;
                Quaternion additionalRotation = Quaternion.Euler(0, 90, 0);
                projectile.transform.rotation = Quaternion.LookRotation(direction) * additionalRotation;
            }
            else
            {
                _targetDirection = Vector3.up * projectile.Speed;
                projectile.transform.rotation = Quaternion.LookRotation(Vector3.up);
            }
        }

        public void Move(BaseProjectile projectile)
        {
            projectile.transform.position += _targetDirection * Time.deltaTime;
        }

        public void Stop()
        {
            _targetDirection = Vector3.zero;
        }
    }
}