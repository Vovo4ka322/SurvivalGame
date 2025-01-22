using System.Collections.Generic;
using UnityEngine;
using EnemyComponents.EnemySettings;
using PlayerComponents;
using Pools;

namespace EnemyComponents
{
    public class EnemyFactory : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField] private PoolSettings _enemyPoolSettings;
        [SerializeField] private PoolSettings _bossPoolSettings;

        [Header("Player Reference")]
        [SerializeField] private Player _player;

        [Header("Max Enemies in Scene")]
        [SerializeField] private int _maxEnemiesInScene = 200;

        private readonly Dictionary<EnemyData, EnemyPool> _enemyPools = new Dictionary<EnemyData, EnemyPool>();
        private int _activeEnemiesCount;

        public bool CanSpawnMore => _maxEnemiesInScene <= 0 || _activeEnemiesCount < _maxEnemiesInScene;

        public void InitializePool(EnemyData enemyData, bool isBoss = false)
        {
            if(enemyData == null) return;

            if(!_enemyPools.ContainsKey(enemyData))
            {
                PoolSettings settings = isBoss ? _bossPoolSettings : _enemyPoolSettings;
                _enemyPools[enemyData] = new EnemyPool(enemyData.EnemyPrefab, settings, container: transform);
            }
        }

        public void SpawnEnemy(EnemyData enemyData, Vector3 position, Quaternion rotation)
        {
            if(enemyData == null || !CanSpawnMore)
            {
                return;
            }

            if(!_enemyPools.TryGetValue(enemyData, out EnemyPool pool))
            {
                pool = new EnemyPool(enemyData.EnemyPrefab, _enemyPoolSettings, container: transform);
                _enemyPools[enemyData] = pool;
            }

            Enemy enemyInstance = pool.Get();

            if(enemyInstance == null)
            {
                return;
            }

            enemyInstance.transform.position = position;
            enemyInstance.transform.rotation = rotation;
            enemyInstance.InitializeComponents(_player, enemyData);

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