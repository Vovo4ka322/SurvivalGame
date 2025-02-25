using System.Collections;
using UnityEngine;
using PlayerComponents;
using Pools;

namespace EnemyComponents.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _projectileEffectPrefab;
        [SerializeField] private ParticleSystem _explosionPrefab;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifetime;
        [SerializeField] private int _damage;

        private Collider _collider;
        private ProjectilePool<BaseProjectile> _pool;
        private IProjectileMovement _movementStrategy;
        private PoolManager _poolManager;

        private float _lifeTimer;
        private bool _hasCollided;
        private bool _isLaunched;

        public float Speed => _speed;
        public float AimHeight { get; private set; } = 1.5f;
        public int Damage => _damage;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            _lifeTimer = _lifetime;
            _hasCollided = false;
            _isLaunched = false;
            _collider.enabled = true;
        }

        private void Update()
        {
            _movementStrategy?.Move(this);

            _lifeTimer -= Time.deltaTime;

            if (_lifeTimer <= 0)
            {
                Explode();
                ReturnToPool();
            }
        }

        public abstract void Launch(Vector3 targetPosition, ProjectilePool<BaseProjectile> pool);

        public void InitializeMovement(IProjectileMovement movementStrategy, ProjectilePool<BaseProjectile> pool)
        {
            _movementStrategy = movementStrategy;
            _pool = pool;
        }

        public void LaunchProjectile(IProjectileMovement movement, Vector3 targetPosition, ProjectilePool<BaseProjectile> pool)
        {
            InitializeMovement(movement, pool);

            _isLaunched = true;

            if (_collider != null)
            {
                _collider.enabled = true;
            }

            ExecuteLaunch(targetPosition);
        }

        public void ExecuteLaunch(Vector3 targetPosition)
        {
            _movementStrategy?.Launch(this, targetPosition);
        }

        public void SetPoolManager(PoolManager poolManager)
        {
            _poolManager = poolManager;
        }

        public void PlayEffects()
        {
            if (_projectileEffectPrefab != null)
            {
                _projectileEffectPrefab.Play();
            }
        }

        private void HandleCollision(Collider other)
        {
            if (_hasCollided || !_isLaunched)
            {
                return;
            }

            _hasCollided = true;

            if (_collider != null)
            {
                _collider.enabled = false;
            }

            if (other.TryGetComponent(out Player player))
            {
                //player.TakeDamage(_projectile.Damage);
            }

            Explode();
            ReturnToPool();
        }

        private void OnCollisionEnter(Collision collision)
        {
            HandleCollision(collision.collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleCollision(other);
        }

        private void Explode()
        {
            if (_poolManager != null && _explosionPrefab != null)
            {
                ParticleSystem explosion = _poolManager.EffectsPool.Get(_explosionPrefab, transform.position, transform.rotation);

                if (explosion != null)
                {
                    _poolManager.CoroutineRunner.StartCoroutine(ReturnEffectToPool(explosion));
                }
            }
        }

        private IEnumerator ReturnEffectToPool(ParticleSystem effect)
        {
            if (effect == null)
            {
                yield break;
            }

            yield return new WaitWhile(() => effect.IsAlive(true));

            _poolManager.EffectsPool.Release(_explosionPrefab, effect);
        }

        private void ReturnToPool()
        {
            _projectileEffectPrefab.Stop();
            _movementStrategy?.Stop();
            transform.localScale = Vector3.one;
            _pool.Release(this);
        }
    }
}