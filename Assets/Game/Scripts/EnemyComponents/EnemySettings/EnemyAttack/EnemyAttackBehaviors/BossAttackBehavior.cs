using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackData;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackBehaviors
{
    public class BossAttackBehavior : IAttackBehavior
    {
        private readonly Enemy _enemy;
        private readonly BossAttackData _bossAttackDataType;

        public BossAttackBehavior(Enemy enemy, BossAttackData bossAttackDataType)
        {
            _enemy = enemy;
            _bossAttackDataType = bossAttackDataType;
        }

        public void HandleAttack(float distance)
        {
            if (distance > _bossAttackDataType.MeleeRange)
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