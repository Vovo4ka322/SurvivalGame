using UnityEngine;
using Game.Scripts.EnemyComponents;
using Game.Scripts.PoolComponents;
using Game.Scripts.ProjectileComponents.CollisionComponents;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;

namespace Game.Scripts.ProjectileComponents
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _projectileEffectPrefab;
        [SerializeField] private ParticleSystem _explosionPrefab;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifetime;
        [SerializeField] private int _damage;

        public BasePool<BaseProjectile> Pool;

        private Collider _collider;
        private IProjectileMovement _movementStrategy;
        private IExplosionHandler _explosionHandler;
        private ProjectileController _projectileController;
        private ProjectileCollisionHandler _projectileCollision;

        public float ConfiguredLifetime => _lifetime;
        public float Speed => _speed;
        public int Damage => _damage;
        public float AimHeight { get; private set; } = 1f;
        public Enemy Owner { get; private set; }

        private void Awake()
        {
            _projectileController = GetComponent<ProjectileController>();
            _projectileCollision = GetComponent<ProjectileCollisionHandler>();
            _collider = GetComponent<Collider>();
        }

        public abstract void Launch(Vector3 targetPosition, BasePool<BaseProjectile> pool, IExplosionHandler explosionHandler);

        public void SetColliderActive(bool active)
        {
            if (_collider != null)
            {
                _collider.enabled = active;
            }
        }

        public void SetOwner(Enemy owner)
        {
            Owner = owner;
        }

        public void Move()
        {
            _movementStrategy?.Move(this);
        }

        public void ExplodeAndReturn()
        {
            _movementStrategy?.Stop();
            _explosionHandler?.Explode(this);
            ReturnToPool();
        }

        public void PlayEffects()
        {
            _projectileEffectPrefab?.Play();
        }

        protected void InitializeProjectile(IProjectileMovement movement, BasePool<BaseProjectile> pool, IExplosionHandler explosionHandler, float lifetime)
        {
            _movementStrategy = movement;
            Pool = pool;
            _explosionHandler = explosionHandler;

            _projectileController.Initialize(this, lifetime);
            _projectileCollision.Initialize(this);
        }

        protected void LaunchProjectile(Vector3 targetPosition)
        {
            _movementStrategy?.Launch(this, targetPosition);
            PlayEffects();
        }

        private void ResetState()
        {
            transform.SetParent(null);
            transform.localScale = Vector3.one;
            _movementStrategy?.Stop();
            SetColliderActive(false);
            Owner = null;
            _projectileController?.ResetController();
        }

        private void ReturnToPool()
        {
            _projectileEffectPrefab?.Stop();
            ResetState();
            Pool?.Release(this);
            gameObject.SetActive(false);
        }
    }
}