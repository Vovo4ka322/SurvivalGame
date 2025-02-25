using EnemyComponents.Interfaces;
using Pools;

namespace EnemyComponents.EnemySettings.Effects
{
    public class SimpleEffect : BaseEffectsEnemy
    {
        public SimpleEffect(ICoroutineRunner coroutineRunner, EffectData effectData, EffectsPool pool) : base(coroutineRunner, effectData, pool)
        {
        }
    }
}