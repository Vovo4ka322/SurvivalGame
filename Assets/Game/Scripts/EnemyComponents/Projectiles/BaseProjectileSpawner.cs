using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttackType;
using Game.Scripts.PoolComponents;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.EnemyComponents.Projectiles
{
    public class BaseProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _projectileSpawnPoint;
        
        private Player _player;
        private EnemyData _enemyData;
        private PoolManager _poolManager;
        
        public Player Player => _player;
        public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
        public ProjectilePool<BaseProjectile> ProjectilePool => _poolManager.GetProjectilePool(GetPrefabFromEnemyData(_enemyData));
        
        public void Initialize(EnemyData data, Player player, PoolManager poolManager)
        {
            _enemyData = data;
            _player = player;
            _poolManager = poolManager;
        }
        
        public BaseProjectile Create()
        {
            if(_poolManager == null || _projectileSpawnPoint == null)
            {
                return null;
            }
            
            BaseProjectile projectilePrefab = GetPrefabFromEnemyData(_enemyData);
            var pool = _poolManager.GetProjectilePool(projectilePrefab);
            
            if(projectilePrefab == null || pool == null)
            {
                return null;
            }
            
            BaseProjectile projectile = pool.Get();
            
            if (projectile == null)
            {
                return null;
            }
            
            projectile.SetPoolManager(_poolManager);
            projectile.transform.position = _projectileSpawnPoint.position;
            projectile.transform.rotation = _projectileSpawnPoint.rotation;
            projectile.transform.localScale = _projectileSpawnPoint.localScale;
            projectile.gameObject.SetActive(true);
            
            return projectile;
        }

        private BaseProjectile GetPrefabFromEnemyData(EnemyData data)
        {
            if(data.BaseAttackType is RangedEnemyAttackType ranged)
            {
                return ranged.ProjectilePrefab;
            }
            
            if(data.BaseAttackType is HybridEnemyAttackType hybrid)
            {
                return hybrid.ProjectilePrefab;
            }
            
            return null;
        }
    }
}