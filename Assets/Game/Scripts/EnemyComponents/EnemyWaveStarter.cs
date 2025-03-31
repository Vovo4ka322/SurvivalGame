using UnityEngine;

namespace Game.Scripts.EnemyComponents
{
    public class EnemyWaveStarter : MonoBehaviour
    {
        [SerializeField] private WaveCycle _waveCycle;
        [SerializeField] private WaveBasedEnemySpawner _spawner;

        private void OnEnable()
        {
            _waveCycle.OnWaveStart += OnWaveStart;
        }

        private void OnDisable()
        {
            _waveCycle.OnWaveStart -= OnWaveStart;
        }

        private void OnWaveStart(float duration, int waveNumber)
        {
            _spawner.StartWave(duration, waveNumber, _waveCycle.SpawnInterval);
        }
    }
}
