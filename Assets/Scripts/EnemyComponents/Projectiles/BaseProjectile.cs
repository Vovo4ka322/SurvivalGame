using UnityEngine;
using Pools;

namespace EnemyComponents.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifetime;
        
        private ProjectilePool<BaseProjectile> _pool;
        private IProjectileMovement _movementStrategy;
        private float _lifeTimer;
        
        public float Speed => _speed;
        public float AimHeight { get; private set; } = 1.5f;
        
        private void OnEnable()
        {
            _lifeTimer = _lifetime;
        }
        
        public virtual void Update()
        {
            _movementStrategy?.Move(this);
            
            _lifeTimer -= Time.deltaTime;
            
            if(_lifeTimer <= 0)
            {
                ReturnToPool();
            }
        }
        
        public abstract void Launch(Vector3 targetPosition, ProjectilePool<BaseProjectile> pool);
        
        public void InitializeMovement(IProjectileMovement movementStrategy, ProjectilePool<BaseProjectile> pool)
        {
            _movementStrategy = movementStrategy;
            SetPool(pool);
        }
        
        public void ExecuteLaunch(Vector3 targetPosition)
        {
            _movementStrategy?.Launch(this, targetPosition);
        }
        
        public void OnCollisionEnter(Collision collision)
        {
            ReturnToPool();
        }
        
        public void SetPool(ProjectilePool<BaseProjectile> pool)
        {
            _pool = pool;
        }
        
        public void PlayEffects()
        {
            if (_particleSystem != null)
            {
                _particleSystem.Play();
            }
        }
        
        private void ReturnToPool()
        {
            _particleSystem.Stop();
            
            if(_pool != null)
            {
                _pool.Release(this);
            }
        }
    }
}