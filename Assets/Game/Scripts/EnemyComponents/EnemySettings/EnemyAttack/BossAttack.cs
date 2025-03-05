using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class BossAttack : IAttackBehavior
    {
        private readonly Enemy _enemy;
        private readonly HybridEnemyAttackType _hybridAttackType;

        public BossAttack(Enemy enemy, HybridEnemyAttackType hybridAttackType)
        {
            _enemy = enemy;
            _hybridAttackType = hybridAttackType;
        }

        public void HandleAttack(float distance)
        {
            if (distance > _hybridAttackType.MeleeRange)
            {
                _enemy.Movement.CanMove(true);
            }
            else
            {
                _enemy.Movement.CanMove(false);
                _enemy.EnemyAttack.TryAttack();
            }
        }
    }
}