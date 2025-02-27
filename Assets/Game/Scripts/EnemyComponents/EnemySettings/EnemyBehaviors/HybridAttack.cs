using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttackType;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class HybridAttack : IAttackBehavior
    {
        private readonly Enemy _enemy;
        private readonly HybridEnemyAttackType _hybridAttackType;
        
        public HybridAttack(Enemy enemy, HybridEnemyAttackType hybridAttackType)
        {
            _enemy = enemy;
            _hybridAttackType = hybridAttackType;
        }
        
        public void HandleAttack(float distance)
        {
            if (distance > _hybridAttackType.RangedRange)
            {
                _enemy.Movement.CanMove(true);
            }
            else if (distance > _hybridAttackType.MeleeRange)
            {
                if (_enemy.EnemyAttack.IsHybridProjectileReady(_hybridAttackType, distance))
                {
                    _enemy.Movement.CanMove(false);
                    _enemy.EnemyAttack.TryAttack();
                }
                else
                {
                    _enemy.Movement.CanMove(true);
                }
            }
            else
            {
                _enemy.Movement.CanMove(false);
                _enemy.EnemyAttack.TryAttack();
            }
        }
    }
}