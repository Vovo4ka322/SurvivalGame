using UnityEngine;

namespace Enemies
{
    [System.Serializable]
    public class EnemyGroup
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }

        [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
    }
}