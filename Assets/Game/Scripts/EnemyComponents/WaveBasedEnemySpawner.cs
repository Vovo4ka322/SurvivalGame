using System.Collections;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.HealthComponents;
using Game.Scripts.MusicComponents;
using Game.Scripts.PoolComponents;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.EnemyComponents
{
    public class WaveBasedEnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameSceneAudio _gameSceneAudio;
        
        [Header("Wave Data")]
        [SerializeField] private EnemyData[] _easyEnemyDatas;
        [SerializeField] private EnemyData[] _mediumEnemyDatas;
        [SerializeField] private EnemyData[] _hardEnemyDatas;
        [SerializeField] private EnemyData _bossEnemyData;
        [SerializeField] private BossHealthViewer _bossHealthViewer;
        
        [Header("Spawn References")]
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _bossSpawnPoint;
        [SerializeField] private PoolManager _poolManager;
        
        private Player _player;
        private ICoroutineRunner _coroutineRunner;
        
        private void Awake()
        {
            _coroutineRunner = _poolManager.GetComponent<ICoroutineRunner>();
        }
        
        public void StartWave(float waveDuration, int waveNumber, float spawnInterval)
        {
            if (waveNumber == 0)
            {
                CreateBoss();
            }
            else
            {
                EnemyData[] enemyDatas = GetEnemyDataByWaveNumber(waveNumber);
                
                _coroutineRunner.StartCoroutine(CreateWave(enemyDatas, waveDuration, spawnInterval));
            }
        }
        
        public void Init(Player player)
        {
            _player = player;
        }
        
        private void CreateBoss()
        {
            _coroutineRunner.StartCoroutine(CreateBossCoroutine());
        }
        
        private EnemyData[] GetEnemyDataByWaveNumber(int waveNumber)
        {
            switch (waveNumber)
            {
                case 1:
                    return _easyEnemyDatas;
                case 2:
                    return _mediumEnemyDatas;
                case 3:
                    return _hardEnemyDatas;
                default:
                    return _easyEnemyDatas;
            }
        }
        
        private IEnumerator CreateWave(EnemyData[] enemyDatas, float waveDuration, float spawnInterval)
        {
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
                
                yield return new WaitForSeconds(spawnInterval);
                
                elapsed += spawnInterval;
            }
        }
        
        private IEnumerator CreateBossCoroutine()
        {
            yield return null;
            
            if (_bossSpawnPoint != null && _bossEnemyData != null)
            {
                Enemy boss = _poolManager.EnemyFactory.SpawnEnemy(_bossEnemyData, _bossSpawnPoint.position, _bossSpawnPoint.rotation, _player);
                
                if (_bossHealthViewer != null)
                {
                    _bossHealthViewer.gameObject.SetActive(true);
                    _bossHealthViewer.Set(boss);
                }
            }
            
            _gameSceneAudio.SwitchToBossMusic();
        }
        
        private Vector3 GetRandomSpawnPosition()
        {
            if(_spawnPoints != null && _spawnPoints.Length > 0)
            {
                int randIndex = Random.Range(0, _spawnPoints.Length);
                
                return _spawnPoints[randIndex].position;
            }
            
            return Vector3.zero;
        }
    }
}