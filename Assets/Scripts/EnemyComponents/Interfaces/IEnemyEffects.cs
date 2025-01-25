using EnemyComponents.EnemySettings;
using Pools;

namespace EnemyComponents.Interfaces
{
    public interface IEnemyEffects
    {
        public void Initialize(EnemyData data, EffectsPool pool, ICoroutineRunner coroutineRunner);
        public void Spawn();
        public void Hit();
        public void Death();
        public void Reload();
        public void StopReload();
        public void Attack(int effectNumber);
    }
}