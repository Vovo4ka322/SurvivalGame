using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Scripts.EnemyComponents.EnemySettings;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.MusicComponents.EffectSounds;
using Game.Scripts.PoolComponents;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.EnemyComponents
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private SoundCollection _soundCollection;
        
        private readonly Dictionary<EnemyData, EnemyPool> _enemyPools = new Dictionary<EnemyData, EnemyPool>();
        private readonly int _maxEnemiesInScene = 200;

        private ICoroutineRunner _coroutineRunner;
        private PoolManager _poolManager;
        private EffectsPool _effectsPool;
        private PoolSettings _poolSettings;
        private Transform _container;

        private int _activeEnemiesCount = 0;
        
        public bool CanSpawnMore => _maxEnemiesInScene <= 0 || _activeEnemiesCount < _maxEnemiesInScene;
        
        public event Action BossDead;
        
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

        public Enemy SpawnEnemy(EnemyData enemyData, Vector3 position, Quaternion rotation, Player player)
        {
            EnemyPool pool;

            if (enemyData == null || !CanSpawnMore)
            {
                return null;
            }

            if (!_enemyPools.TryGetValue(enemyData, out pool))
            {
                pool = new EnemyPool(enemyData.EnemyPrefab, _poolSettings, _container);
                _enemyPools.Add(enemyData, pool);
            }

            Enemy enemyInstance = pool.Get();

            if (enemyInstance == null)
            {
                return null;
            }
            
            float sampleRadius = 10f;
            
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, sampleRadius, NavMesh.AllAreas))
            {
                enemyInstance.transform.position = hit.position;

                if(enemyInstance.TryGetComponent(out NavMeshAgent agent))
                {
                    agent.Warp(hit.position);
                }
            }
            else
            {
                enemyInstance.transform.position = position;
            }
            
            enemyInstance.transform.rotation = rotation;
            enemyInstance.ResetState();
            enemyInstance.SetSoundCollection(_soundCollection);
            enemyInstance.InitializeComponents(player, enemyData, _effectsPool, _poolManager, _coroutineRunner);
            enemyInstance.TurnOnAgent();
            enemyInstance.Enabled += OnEnemyEnabled;
            enemyInstance.Dead += OnEnemyDisabled;
            
            return enemyInstance;
        }

        private void OnEnemyEnabled(Enemy enemy)
        {
            _activeEnemiesCount++;
        }

        private void OnEnemyDisabled(Enemy enemy)
        {
            enemy.Enabled -= OnEnemyEnabled;
            enemy.Dead -= OnEnemyDisabled;

            _activeEnemiesCount--;

            if (_activeEnemiesCount < 0)
            {
                _activeEnemiesCount = 0;
            }

            if(_enemyPools.TryGetValue(enemy.Data, out EnemyPool pool))
            {
                pool.Release(enemy);
                enemy.TurnOffAgent();

                if(enemy.Data.EnemyType == EnemyType.Boss)
                {
                    BossDead?.Invoke();
                }
            }
        }
    }
}