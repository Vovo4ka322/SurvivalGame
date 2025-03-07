using Game.Scripts.EnemyComponents;
using Game.Scripts.Interfaces;
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

            if (other.TryGetComponent(out IDamagable player))
            {
                player.TakeDamage(_projectile.Damage);
            }

            _projectile.ExplodeAndReturn();
        }
    }
}