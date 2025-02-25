using UnityEngine;
using EnemyComponents;

namespace Pools
{
    public class EnemyPool : BasePool<Enemy>
    {
        public EnemyPool(Enemy prefab, PoolSettings settings, Transform container = null) : base(prefab, settings, container)
        {
        }
    }
}