using System.Collections;
using UnityEngine;
using EnemyComponents.EnemySettings;
using EnemyComponents.Interfaces;
using PlayerComponents;
using Pools;

namespace EnemyComponents
{
    public class WaveBasedEnemySpawner : MonoBehaviour
    {
        [Header("Wave Data")]
        [SerializeField] private EnemyData[] _easyEnemyDatas;
        [SerializeField] private EnemyData[] _mediumEnemyDatas;
        [SerializeField] private EnemyData[] _hardEnemyDatas;
        [SerializeField] private EnemyData _bossEnemyData;
        
        [Header("Wave Durations (seconds)")]
        [SerializeField] private float _easyWaveDuration = 21f * 60f;
        [SerializeField] private float _mediumWaveDuration = 14f * 60f;
        [SerializeField] private float _hardWaveDuration = 7f * 60f;
        
        [Header("Wave Start Delays (seconds)")]
        [SerializeField] private float _startDelayEasyWave = 0f;
        [SerializeField] private float _startDelayMediumWave = 7f * 60f;
        [SerializeField] private float _startDelayHardWave = 14f * 60f;
        [SerializeField] private float _bossSpawnDelay = 19f * 60f;
        [SerializeField] private float _spawnInterval = 5f;
        
        [Header("Spawn References")]
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _bossSpawnPoint;
        [SerializeField] private Collider _spawnZone;
        [SerializeField] private PoolManager _poolManager;
        
        private Player _player;
        private ICoroutineRunner _coroutineRunner;
        private Coroutine _easyWaveCoroutine;
        private Coroutine _mediumWaveCoroutine;
        private Coroutine _hardWaveCoroutine;
        private Coroutine _bossCoroutine;
        
        private bool _playerInZone;
        
        private void Awake()
        {
            _coroutineRunner = _poolManager.GetComponent<ICoroutineRunner>();
        }

        private void Start()
        {
            _easyWaveCoroutine = _coroutineRunner.StartCoroutine(CreateWave(_easyEnemyDatas, _startDelayEasyWave, _easyWaveDuration));
            _mediumWaveCoroutine = _coroutineRunner.StartCoroutine(CreateWave(_mediumEnemyDatas, _startDelayMediumWave, _mediumWaveDuration));
            _hardWaveCoroutine = _coroutineRunner.StartCoroutine(CreateWave(_hardEnemyDatas, _startDelayHardWave, _hardWaveDuration));
            _bossCoroutine = _coroutineRunner.StartCoroutine(CreateBoss(_bossSpawnDelay));
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player _) && !_playerInZone)
            {
                _playerInZone = true;
                
                _easyWaveCoroutine = _coroutineRunner.StartCoroutine(CreateWave(_easyEnemyDatas, _startDelayEasyWave, _easyWaveDuration));
                _mediumWaveCoroutine = _coroutineRunner.StartCoroutine(CreateWave(_mediumEnemyDatas, _startDelayMediumWave, _mediumWaveDuration));
                _hardWaveCoroutine = _coroutineRunner.StartCoroutine(CreateWave(_hardEnemyDatas, _startDelayHardWave, _hardWaveDuration));
                _bossCoroutine = _coroutineRunner.StartCoroutine(CreateBoss(_bossSpawnDelay));
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out Player _))
            {
                _playerInZone = false;

                if(_easyWaveCoroutine != null)
                {
                    _coroutineRunner.StopCoroutine(_easyWaveCoroutine);
                }

                if(_mediumWaveCoroutine != null)
                {
                    _coroutineRunner.StopCoroutine(_mediumWaveCoroutine);
                }

                if(_hardWaveCoroutine != null)
                {
                    _coroutineRunner.StopCoroutine(_hardWaveCoroutine);
                }

                if(_bossCoroutine != null)
                {
                    _coroutineRunner.StopCoroutine(_bossCoroutine);
                }
                
                _easyWaveCoroutine = null;
                _mediumWaveCoroutine = null;
                _hardWaveCoroutine = null;
                _bossCoroutine = null;
            }
        }
        
        public void Init(Player player)
        {
            _player = player;
        }

        private IEnumerator CreateWave(EnemyData[] enemyDatas, float startDelay, float waveDuration)
        {
            yield return new WaitForSeconds(startDelay);
            
            float elapsed = 0f;
            
            while (elapsed < waveDuration)
            {
                while (!_poolManager.EnemyFactory.CanSpawnMore)
                {
                    yield return null;
                }
                
                EnemyData randomData = enemyDatas[Random.Range(0, enemyDatas.Length)];
                Vector3 spawnPos = GetRandomSpawnPosition();
                Quaternion spawnRot = Quaternion.identity;
                
                _poolManager.EnemyFactory.SpawnEnemy(randomData, spawnPos, spawnRot, _player);
                
                yield return new WaitForSeconds(_spawnInterval);
                
                elapsed += _spawnInterval;
            }
        }
        
        private IEnumerator CreateBoss(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            if (_bossSpawnPoint != null && _bossEnemyData != null)
            {
                _poolManager.EnemyFactory.SpawnEnemy(_bossEnemyData, _bossSpawnPoint.position, _bossSpawnPoint.rotation, _player);
            }
        }
        
        private Vector3 GetRandomSpawnPosition()
        {
            if(_spawnPoints != null && _spawnPoints.Length > 0)
            {
                int randIndex = Random.Range(0, _spawnPoints.Length);
                
                return _spawnPoints[randIndex].position;
            }
            
            if(_spawnZone != null)
            {
                Bounds bounds = _spawnZone.bounds;
                
                float x = Random.Range(bounds.min.x, bounds.max.x);
                float z = Random.Range(bounds.min.z, bounds.max.z);
                float y = bounds.min.y;
                
                return new Vector3(x, y, z);
            }
            
            return Vector3.zero;
        }
    }
}