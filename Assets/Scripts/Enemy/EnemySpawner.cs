using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyWave> _waves = new();
        [SerializeField] private Player _player;
        [SerializeField] private Timer _timer;
        [SerializeField] private List<EnemyData> _enemiesData = new();

        private Dictionary<EnemyType, EnemyData> _enemyTypeData = new();
        private Coroutine _coroutine;
        private int _currentWaveIndex = 0;

        private void Awake()
        {
            SetWaveTime();
        }

        private void OnEnable()
        {
            _timer.WaveChanged += OnWaveChanged;
        }

        private void Start()
        {
            foreach (var data in _enemiesData)
            {
                Add(data);
            }

            SpawnWaveCoroutine();
        }

        private void OnDisable()
        {
            _timer.WaveChanged -= OnWaveChanged;
        }

        private IEnumerator SpawnWave()
        {
            EnemyWave wave = _waves[_currentWaveIndex];

            WaitForSeconds delay = new(wave.SpawnInterval);

            for (int i = 0; i < wave.MaxEnemyCount; i++)
            {
                foreach (var group in wave.Groups)
                {
                    Spawn(group.EnemyPrefab, group.EnemyType);
                }

                yield return delay;
            }
        }

        private void OnWaveChanged()
        {
            if (_currentWaveIndex == _waves.Count - 1)
                return;

            _currentWaveIndex++;
            SetWaveTime();

            StopCoroutine(_coroutine);
            SpawnWaveCoroutine();
        }

        private void SpawnWaveCoroutine()
        {
            _coroutine = StartCoroutine(SpawnWave());
        }

        private void Add(EnemyData enemyData)
        {
            _enemyTypeData.Add(enemyData.EnemyType, enemyData);
        }

        private void Spawn(Enemy enemy, EnemyType enemyType)
        {
            Vector3 spawnPosition = new(_player.transform.position.x + Random.Range(-10f, 10f), 1f, _player.transform.position.z + Random.Range(-10f, 10f));

            Enemy newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);

            newEnemy.Init(_enemyTypeData[enemyType], _player.transform);

            if (EnemyType.Boss == enemyType)
            {
                //Будет логика сражения с боссом. (Отдаем боссу того, кому он скажет, что умер).
            }
        }

        private void SetWaveTime() => _timer.SetWaveTime(_waves[_currentWaveIndex].WaveTime);
    }
}