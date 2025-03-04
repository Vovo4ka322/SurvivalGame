using Game.Scripts.EnemyComponents;
using Game.Scripts.PlayerComponents;
using UnityEngine;

namespace Game.Scripts.ProjectileComponents.CollisionComponents
{
    public class ProjectileCollisionHandler : MonoBehaviour
    {
        private BaseProjectile _projectile;
        private Collider _collider;
        
        private bool _hasCollided = false;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Initialize(BaseProjectile projectile)
        {
            _projectile = projectile;
        }

        private void OnCollisionEnter(Collision collision)
        {
            HandleCollision(collision.collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleCollision(other);
        }

        private void HandleCollision(Collider other)
        {
            if (_hasCollided)
            {
                return;
            }

            _hasCollided = true;
            _collider.enabled = false;

            if (_projectile is ArrowProjectile)
            {
                if (other.TryGetComponent(out Enemy enemy))
                {
                    enemy.Health.Lose(_projectile.Damage);
                }
            }
            else
            {
                 if (other.TryGetComponent(out Player player))
                 {
                     player.LoseHealth(_projectile.Damage);
                 }
            }
            
            _projectile.ExplodeAndReturn();
        }
    }
}