using Game.Scripts.EnemyComponents.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.EnemyComponents
{
    public class WaveCycle : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _coroutineRunnerComponent;

        [Header("Wave Durations (seconds)")]
        [SerializeField] private float _easyWaveDuration = 21f * 60f;
        [SerializeField] private float _mediumWaveDuration = 14f * 60f;
        [SerializeField] private float _hardWaveDuration = 7f * 60f;
        [SerializeField] private float _spawnInterval = 5f;

        private ICoroutineRunner _coroutineRunner;
        private int _currentWaveIndex = 0;

        public event Action<float, int> OnWaveStart;

        public float SpawnInterval => _spawnInterval;

        private void Awake()
        {
            _coroutineRunner = _coroutineRunnerComponent as ICoroutineRunner;
        }

        private void Start()
        {
            _coroutineRunner.StartCoroutine(ExecuteWaves());
        }

        private IEnumerator ExecuteWaves()
        {
            while (_currentWaveIndex < 3)
            {
                float duration = GetCurrentWaveDuration();

                OnWaveStart?.Invoke(duration, _currentWaveIndex + 1);

                yield return _coroutineRunner.StartCoroutine(WaitWaveDuration(duration));

                _currentWaveIndex++;
            }

            OnWaveStart?.Invoke(0, 0);
        }

        private IEnumerator WaitWaveDuration(float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                yield return new WaitForSeconds(_spawnInterval);

                elapsed += _spawnInterval;
            }
        }

        private float GetCurrentWaveDuration()
        {
            switch (_currentWaveIndex)
            {
                case 0: return _easyWaveDuration;
                case 1: return _mediumWaveDuration;
                case 2: return _hardWaveDuration;
                default: return _easyWaveDuration;
            }
        }
    }
}