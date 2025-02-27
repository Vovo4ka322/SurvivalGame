using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.EnemyComponents;
using Game.Scripts.EnemyComponents.EnemySettings;
using Game.Scripts.EnemyComponents.Projectiles;

namespace Game.Scripts.PoolComponents
{
    public class PoolManager : MonoBehaviour
    {
        private const string CoroutineRunnerName = "CoroutineRunner";
        
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private EnemyFactory _enemyFactory;
        
        [Header("Pool Settings")]
        [SerializeField] private PoolSettings _defaultPoolSettings;

        [Header("Prefabs")]
        [SerializeField] private List<EnemyData> _enemyDatas;
        [SerializeField] private List<BaseProjectile> _projectilePrefabs;
        [SerializeField] private List<ParticleSystem> _effectPrefabs;
        
        private EffectsPool _effectsPool;
        private Dictionary<BaseProjectile, ProjectilePool<BaseProjectile>> _projectilePools;

        public EnemyFactory EnemyFactory => _enemyFactory;
        public EffectsPool EffectsPool => _effectsPool;
        public CoroutineRunner CoroutineRunner => _coroutineRunner;

        private void Awake()
        {
            if (_coroutineRunner == null)
            {
                GameObject coroutineRunnerObj = new GameObject(CoroutineRunnerName);
                _coroutineRunner = coroutineRunnerObj.AddComponent<CoroutineRunner>();
                
                DontDestroyOnLoad(coroutineRunnerObj);
            }
            
            InitializeEffectsPool();
            InitializeEnemyFactory();
            InitializeProjectilePools();
        }

        public ProjectilePool<BaseProjectile> GetProjectilePool(BaseProjectile prefab)
        {
            if (prefab == null)
            {
                throw new ArgumentException("GetProjectilePool was called with a null prefab.");
            }
            
            return _projectilePools.GetValueOrDefault(prefab);
        }
        
        private void InitializeEffectsPool()
        {
            _effectsPool = new EffectsPool();
            _effectsPool.Initialize(_effectPrefabs, _defaultPoolSettings, transform);
        }

        private void InitializeEnemyFactory()
        {
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