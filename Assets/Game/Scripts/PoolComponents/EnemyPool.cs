using UnityEngine;
using Game.Scripts.EnemyComponents;

namespace Game.Scripts.PoolComponents
{
    public class EnemyPool : BasePool<Enemy>
    {
        public EnemyPool(Enemy prefab, PoolSettings settings, Transform container = null)
            : base(prefab, settings, container)
        {
        }
    }
}