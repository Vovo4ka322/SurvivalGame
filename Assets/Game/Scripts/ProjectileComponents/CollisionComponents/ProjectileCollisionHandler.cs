using Game.Scripts.EnemyComponents;
using Game.Scripts.PlayerComponents;
using UnityEngine;

namespace Game.Scripts.ProjectileComponents.CollisionComponents
{
    public class ProjectileCollisionHandler : MonoBehaviour
    {
        private const string GorundNameLayer = "Floor";
        
        private BaseProjectile _projectile;
        private Collider _collider;
        
        private bool _hasCollided = false;
        private int _groundLayer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _groundLayer = LayerMask.NameToLayer(GorundNameLayer);
        }

        public void Initialize(BaseProjectile projectile)
        {
            _projectile = projectile;
            _hasCollided = false;
            _collider.enabled = true; 
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
            
            if (other.gameObject.layer == _groundLayer)
            {
                _hasCollided = true;
                _collider.enabled = false;
                _projectile.ExplodeAndReturn();
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