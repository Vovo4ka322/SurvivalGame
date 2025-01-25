using System.Collections.Generic;
using EnemyComponents;
using EnemyComponents.EnemySettings;
using EnemyComponents.Projectiles;
using UnityEngine;

namespace Pools
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        [Header("Pool Settings")]
        [SerializeField] private PoolSettings _defaultPoolSettings;

        [Header("Prefabs")]
        [SerializeField] private List<EnemyData> _enemyDatas;
        [SerializeField] private List<BaseProjectile> _projectilePrefabs;
        [SerializeField] private List<ParticleSystem> _effectPrefabs;
        
        private EnemyFactory _enemyFactory;
        private EffectsPool _effectsPool;
        private Dictionary<BaseProjectile, ProjectilePool<BaseProjectile>> _projectilePools;

        public EnemyFactory EnemyFactory => _enemyFactory;
        public EffectsPool EffectsPool => _effectsPool;

        private void Awake()
        {
            if (_coroutineRunner == null)
            {
                GameObject coroutineRunnerObj = new GameObject("CoroutineRunner");
                _coroutineRunner = coroutineRunnerObj.AddComponent<CoroutineRunner>();
                
                DontDestroyOnLoad(coroutineRunnerObj);
            }
            
            InitializeEffectsPool();
            InitializeEnemyFactory();
            InitializeProjectilePools();
        }

        public ProjectilePool<BaseProjectile> GetProjectilePool(BaseProjectile prefab)
        {
            return _projectilePools.GetValueOrDefault(prefab);
        }
        
        private void InitializeEffectsPool()
        {
            _effectsPool = new EffectsPool();
            _effectsPool.Initialize(_effectPrefabs, _defaultPoolSettings, transform);
        }

        private void InitializeEnemyFactory()
        {
            _enemyFactory = gameObject.AddComponent<EnemyFactory>();
            _enemyFactory.Initialize(_enemyDatas, _defaultPoolSettings, _effectsPool, transform, this, _coroutineRunner);
        }

        private void InitializeProjectilePools()
        {
            _projectilePools = new Dictionary<BaseProjectile, ProjectilePool<BaseProjectile>>();
            
            foreach (BaseProjectile prefab in _projectilePrefabs)
            {
                if (prefab != null && !_projectilePools.ContainsKey(prefab))
                {
                    var pool = new ProjectilePool<BaseProjectile>(prefab, _defaultPoolSettings, transform);
                    
                    _projectilePools.Add(prefab, pool);
                }
            }
        }
    }
}