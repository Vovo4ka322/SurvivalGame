using EnemyComponents.EnemySettings.EnemyAttackType;

namespace EnemyComponents.Interfaces
{
    public interface IEnemyAttack
    {
        public void TryAttack();
        public bool IsHybridProjectileReady(HybridEnemyAttackType hybridType, float distance);
    }
}