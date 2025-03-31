using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackData;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackBehaviors
{
    public class HybridAttackBehavior : IAttackBehavior
    {
        private readonly Enemy _enemy;
        private readonly HybridAttackData _hybridAttackDataType;

        public HybridAttackBehavior(Enemy enemy, HybridAttackData hybridAttackDataType)
        {
            _enemy = enemy;
            _hybridAttackDataType = hybridAttackDataType;
        }

        public void HandleAttack(float distance)
        {
            if (distance > _hybridAttackDataType.RangedRange)
            {
                _enemy.Movement.CanMove(true);
            }
            else if (distance > _hybridAttackDataType.MeleeRange)
            {
                if (_enemy.EnemyAttack.IsHybridProjectileReady(_hybridAttackDataType, distance))
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