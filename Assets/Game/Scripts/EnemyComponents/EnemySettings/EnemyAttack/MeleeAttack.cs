using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class MeleeAttack : IAttackBehavior
    {
        private readonly Enemy _enemy;

        public MeleeAttack(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void HandleAttack(float distance)
        {
            if (distance > _enemy.Data.AttackRange)
            {
                _enemy.SetTargetPosition(_enemy.PlayerTransform.transform.position);
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