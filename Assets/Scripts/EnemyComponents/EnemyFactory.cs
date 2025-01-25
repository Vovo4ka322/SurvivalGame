using System.Collections.Generic;
using UnityEngine;
using EnemyComponents.EnemySettings;
using EnemyComponents.Interfaces;
using PlayerComponents;
using Pools;

namespace EnemyComponents
{
    public class EnemyFactory : MonoBehaviour
    {
        private readonly Dictionary<EnemyData, EnemyPool> _enemyPools = new Dictionary<EnemyData, EnemyPool>();
        private readonly int _maxEnemiesInScene = 200;
        
        private ICoroutineRunner _coroutineRunner;
        private PoolManager _poolManager;
        private EffectsPool _effectsPool;
        private PoolSettings _poolSettings;
        private Transform _container;
        private Player _player;
        
        private int _activeEnemiesCount = 0;
        
        public bool CanSpawnMore => _maxEnemiesInScene <= 0 || _activeEnemiesCount < _maxEnemiesInScene;
        
        public void Initialize(List<EnemyData> enemyDatas, PoolSettings poolSettings, EffectsPool effectsPool, Transform container, PoolManager poolManager, ICoroutineRunner coroutineRunner)
        {
            _poolSettings = poolSettings;
            _effectsPool = effectsPool;
            _container = container;
            _poolManager = poolManager;
            _coroutineRunner = coroutineRunner;
            
            foreach (EnemyData data in enemyDatas)
            {
                if (data != null && !_enemyPools.ContainsKey(data))
                {
                    EnemyPool pool = new EnemyPool(data.EnemyPrefab, poolSettings, container);
                    _enemyPools.Add(data, pool);
                }
            }
        }
        
        public void SpawnEnemy(EnemyData enemyData, Vector3 position, Quaternion rotation, Player player)
        {
            if(enemyData == null || !CanSpawnMore)
            {
                return;
            }
            
            if (!_enemyPools.TryGetValue(enemyData, out var pool))
            {
                pool = new EnemyPool(enemyData.EnemyPrefab, _poolSettings, container: null);
                _enemyPools.Add(enemyData, pool);
            }
            
            Enemy enemyInstance = pool.Get();
            
            if(enemyInstance == null)
            {
                return;
            }
            
            enemyInstance.transform.position = position;
            enemyInstance.transform.rotation = rotation;
            enemyInstance.InitializeComponents(player, enemyData, _effectsPool, _poolManager, _coroutineRunner);
            
            enemyInstance.Enabled += OnEnemyEnabled;
            enemyInstance.Dead += OnEnemyDisabled;
        }
        
        private void OnEnemyEnabled(Enemy enemy)
        {
            _activeEnemiesCount++;
        }
        
        private void OnEnemyDisabled(Enemy enemy)
        {
            _activeEnemiesCount--;
            
            if(_activeEnemiesCount < 0)
            {
                _activeEnemiesCount = 0;
            }
            
            enemy.Enabled -= OnEnemyEnabled;
            enemy.Dead -= OnEnemyDisabled;
        }
    }
}