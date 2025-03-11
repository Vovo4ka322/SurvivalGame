using UnityEngine;
using Game.Scripts.EnemyComponents;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;
using Game.Scripts.Interfaces;

namespace Game.Scripts.ProjectileComponents.CollisionComponents
{
    public class ProjectileCollisionHandler : MonoBehaviour
    {
        private const string GroundNameLayer = "Floor";
        
        private BaseProjectile _projectile;
        private Collider _collider;

        private bool _hasCollided = false;
        private int _groundLayer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _groundLayer = LayerMask.NameToLayer(GroundNameLayer);
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
            if (other.TryGetComponent(out Enemy _))
            {
                return;
            }
            
            if (_hasCollided)
            {
                return;
            }
            
            _hasCollided = true;
            _collider.enabled = false;

            if (_projectile.Owner != null && _projectile.Owner.SoundCollection != null)
            {
                if (_projectile.Owner.Data.BaseAttackType.Type == AttackType.Ranged)
                {
                    _projectile.Owner.SoundCollection.RangedSoundEffects.PlayProjectileCollision();
                }
                else if (_projectile.Owner.Data.BaseAttackType.Type == AttackType.Hybrid)
                {
                    _projectile.Owner.SoundCollection.HybridSoundEffects.PlayProjectileCollision();
                }
            }
            
            if (other.gameObject.layer == _groundLayer)
            {
                _projectile.ExplodeAndReturn();
                return;
            }

            if (other.TryGetComponent(out IDamagable player))
            {
                player.TakeDamage(_projectile.Damage);
            }

            _projectile.ExplodeAndReturn();
        }
    }
}