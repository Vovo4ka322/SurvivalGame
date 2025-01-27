using EnemyComponents.Interfaces;

namespace EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class RangedAttack : IAttackBehavior
    {
        private readonly Enemy _enemy;
        
        public RangedAttack(Enemy enemy)
        {
            _enemy = enemy;
        }
        
        public void HandleAttack(float distance)
        {
            if (distance > _enemy.Data.AttackRange)
            {
                _enemy.PlayerNavigator.MoveTowardsPlayer();
            }
            else
            {
                _enemy.Movement.StopMove();
                _enemy.EnemyAttack.TryAttack();
            }
        }
    }
}