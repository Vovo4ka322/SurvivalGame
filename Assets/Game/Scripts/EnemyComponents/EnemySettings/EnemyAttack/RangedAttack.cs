using UnityEngine;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
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
            float attackRange = _enemy.Data.AttackRange;

            if (distance > attackRange)
            {
                Vector3 direction = (_enemy.transform.position - _enemy.PlayerTransform.transform.position).normalized;
                Vector3 targetPosition = _enemy.PlayerTransform.transform.position + direction * attackRange;

                _enemy.SetTargetPosition(targetPosition);
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