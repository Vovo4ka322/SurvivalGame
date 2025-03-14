using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;

namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface IEnemyAttack
    {
        public void TryAttack();
        
        public bool IsHybridProjectileReady(HybridEnemyAttackType hybridType, float distance);
    }
}