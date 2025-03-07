using System.Collections;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class EnemyAttackExecutor
    {
        private readonly Enemy _enemy;

        public EnemyAttackExecutor(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void SetAttackBehavior()
        {
            switch (_enemy.Data.BaseAttackType.Type)
            {
                case AttackType.Hybrid:
                    HybridEnemyAttackType hybridAttackType = _enemy.Data.BaseAttackType as HybridEnemyAttackType;
                    if (hybridAttackType != null)
                    {
                        if (_enemy.Data.EnemyType == EnemyType.Boss)
                        {
                            _enemy.SetAttackBehaviorInternal(new BossAttack(_enemy, hybridAttackType));
                        }
                        else
                        {
                            _enemy.SetAttackBehaviorInternal(new HybridAttack(_enemy, hybridAttackType));
                        }
                    }
                    else
                    {
                        _enemy.SetAttackBehaviorInternal(new MeleeAttack(_enemy));
                    }
                    break;
                case AttackType.Ranged:
                    _enemy.SetAttackBehaviorInternal(new RangedAttack(_enemy));
                    break;
                default:
                    _enemy.SetAttackBehaviorInternal(new MeleeAttack(_enemy));
                    break;
            }
        }

        public IEnumerator AttackCoroutine()
        {
            float updateInterval = 0.1f;
            
            while (true)
            {
                if(_enemy.Health.IsDead)
                {
                    yield break;
                }
                
                if (_enemy.PlayerTransform != null && _enemy.SpawnCompleted && !_enemy.AnimationAnimationState.IsAttacking)
                {
                    float distance = Vector3.Distance(_enemy.transform.position, _enemy.PlayerTransform.transform.position);
                    
                    _enemy.AttackBehavior.HandleAttack(distance);
                }
                
                yield return new WaitForSeconds(updateInterval);
            }
        }
    }
}