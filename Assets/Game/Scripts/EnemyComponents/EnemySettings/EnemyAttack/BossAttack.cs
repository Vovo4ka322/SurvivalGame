using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class BossAttack : IAttackBehavior
    {
        private readonly Enemy _enemy;
        private readonly EnemyAttackType.BossAttack _bossAttackType;

        public BossAttack(Enemy enemy, EnemyAttackType.BossAttack bossAttackType)
        {
            _enemy = enemy;
            _bossAttackType = bossAttackType;
        }

        public void HandleAttack(float distance)
        {
            if (distance > _bossAttackType.MeleeRange)
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