using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.PoolComponents;

namespace Game.Scripts.EnemyComponents.EnemySettings.Effects
{
    public class SimpleEffect : BaseEffectsEnemy
    {
        public SimpleEffect(ICoroutineRunner coroutineRunner, EffectData effectData, EffectsPool pool) : base(coroutineRunner, effectData, pool)
        {
        }
    }
}