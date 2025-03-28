using Game.Scripts.EnemyComponents.EnemySettings;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;
using Game.Scripts.PoolComponents;
using Game.Scripts.PlayerComponents;
using Game.Scripts.ProjectileComponents.CollisionComponents;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;
using UnityEngine;

namespace Game.Scripts.ProjectileComponents.CreateProjectiles
{
    public class BaseProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private ParticleSystem _explosionPrefab;

        private Player _player;
        private EnemyData _enemyData;
        private PoolManager _poolManager;
        private BaseProjectile _cachedProjectilePrefab;
        private ProjectilePool<BaseProjectile> _cachedProjectilePool;

        public Player Player => _player;
        public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
        public ProjectilePool<BaseProjectile> ProjectilePool => _poolManager.GetProjectilePool(GetPrefabFromEnemyData(_enemyData));

        public void Initialize(EnemyData data, Player player, PoolManager poolManager)
        {
            _enemyData = data;
            _player = player;
            _poolManager = poolManager;

            _cachedProjectilePrefab = GetPrefabFromEnemyData(_enemyData);

            if (_cachedProjectilePrefab != null)
            {
                _cachedProjectilePool = _poolManager.GetProjectilePool(_cachedProjectilePrefab);
            }
        }

        protected BaseProjectile Create()
        {
            if (_poolManager == null || _projectileSpawnPoint == null)
            {
                return null;
            }

            BaseProjectile projectile = _cachedProjectilePool.Get();

            if (projectile == null)
            {
                return null;
            }

            projectile.transform.position = _projectileSpawnPoint.position;
            projectile.transform.rotation = _projectileSpawnPoint.rotation;
            projectile.transform.localScale = _projectileSpawnPoint.localScale;
            projectile.gameObject.SetActive(true);
            return projectile;
        }

        protected IExplosionHandler CreateExplosionHandler()
        {
            return new ExplosionHandler(_poolManager, _explosionPrefab);
        }

        private BaseProjectile GetPrefabFromEnemyData(EnemyData data)
        {
            if (data.BaseAttackType is RangeEnemyAttackSetter ranged)
            {
                return ranged.ProjectilePrefab;
            }

            if (data.BaseAttackType is HybridEnemyAttackSetter hybrid)
            {
                return hybrid.ProjectilePrefab;
            }

            return null;
        }
    }
}