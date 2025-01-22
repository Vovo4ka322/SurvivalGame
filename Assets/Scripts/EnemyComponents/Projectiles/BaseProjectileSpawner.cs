using UnityEngine;
using EnemyComponents.EnemySettings;
using EnemyComponents.EnemySettings.EnemyAttackType;
using PlayerComponents;
using Pools;

namespace EnemyComponents.Projectiles
{
    public class BaseProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private PoolSettings _projectilePoolSettings;
        
        private ProjectilePool<BaseProjectile> _projectilePool;
        private Player _player;
        private EnemyData _enemyData;
        
        public BaseProjectile SpawnProjectile()
        {
            if(ProjectilePool == null || ProjectileSpawnPoint == null)
            {
                return null;
            }
            
            BaseProjectile projectile = ProjectilePool.Get();
            projectile.transform.SetParent(null);
            projectile.transform.position = ProjectileSpawnPoint.position;
            
            return projectile;
        }
        
        public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
        public ProjectilePool<BaseProjectile> ProjectilePool => _projectilePool;
        public Player Player => _player;
        
        public void Initialize(EnemyData data, Player player)
        {
            _enemyData = data;
            _player = player;
            
            BaseProjectile projectilePrefab = null;
            
            if(data.BaseAttackType.Type == AttackType.Ranged) 
            {
                RangedEnemyAttackType ranged = data.BaseAttackType as RangedEnemyAttackType;
                projectilePrefab = ranged?.ProjectilePrefab;
            }
            else if(data.BaseAttackType.Type == AttackType.Hybrid) 
            {
                HybridEnemyAttackType hybrid = data.BaseAttackType as HybridEnemyAttackType;
                projectilePrefab = hybrid?.ProjectilePrefab;
            }
            
            if(projectilePrefab != null)
            {
                _projectilePool = new ProjectilePool<BaseProjectile>(projectilePrefab, _projectilePoolSettings, container: transform);
            }
        }
    }
}