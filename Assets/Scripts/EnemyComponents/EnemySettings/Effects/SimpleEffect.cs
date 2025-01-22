using UnityEngine;
using Pools;

namespace EnemyComponents.EnemySettings.Effects
{
    public class SimpleEffect : BaseEffectsEnemy
    {
        public SimpleEffect(MonoBehaviour owner, EffectData effectData, PoolSettings poolSettings) : base(owner, effectData, poolSettings)
        {
        }
    }
}