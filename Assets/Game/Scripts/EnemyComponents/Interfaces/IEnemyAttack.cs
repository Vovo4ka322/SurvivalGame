using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackData;

namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface IEnemyAttack
    {
        public void TryAttack();
        
        public bool IsHybridProjectileReady(HybridAttackData hybridType, float distance);
    }
}