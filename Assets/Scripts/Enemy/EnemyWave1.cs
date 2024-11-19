using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [System.Serializable]
    public class EnemyWave
    {
        [SerializeField] private List<EnemyGroup> _groups = new();

        public IReadOnlyList<EnemyGroup> Groups => _groups;

        [field: SerializeField] public float SpawnInterval { get; private set; }

        [field: SerializeField] public int MaxEnemyCount { get; private set; }

        [field: SerializeField] public float WaveTime { get; private set; }
    }
}