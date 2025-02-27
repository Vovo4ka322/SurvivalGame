using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
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