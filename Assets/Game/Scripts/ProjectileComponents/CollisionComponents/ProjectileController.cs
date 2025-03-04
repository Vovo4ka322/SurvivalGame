using UnityEngine;

namespace Game.Scripts.ProjectileComponents.CollisionComponents
{
    public class ProjectileController : MonoBehaviour
    {
        private BaseProjectile _projectile;
        
        private float _lifeTimer;
        private bool _isLaunched = false;
        
        public void Initialize(BaseProjectile projectile, float lifetime)
        {
            _projectile = projectile;
            _lifeTimer = lifetime;
            _isLaunched = true;
        }

        private void Update()
        {
            if(!_isLaunched)
            {
                return;
            }
            
            if (_projectile.Owner != null && _projectile.Owner.Health != null && _projectile.Owner.Health.IsDead)
            {
                _projectile.ExplodeAndReturn();
                _isLaunched = false;
                
                return;
            }

            _lifeTimer -= Time.deltaTime;
            
            if (_lifeTimer <= 0)
            {
                _projectile.ExplodeAndReturn();
                _isLaunched = false;
            }
            else
            {
                _projectile.Move();
            }
        }
    }
}