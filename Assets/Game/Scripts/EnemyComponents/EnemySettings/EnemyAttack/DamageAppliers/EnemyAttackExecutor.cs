using System.Collections;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackBehaviors;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackData;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.DamageAppliers
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
            switch (_enemy.Data.BaseAttackData.AttackType)
            {
                case AttackType.Hybrid:
                    HybridAttackData hybridAttackDataType = _enemy.Data.BaseAttackData as HybridAttackData;

                    if (hybridAttackDataType != null)
                    {
                        _enemy.SetAttackBehaviorInternal(new HybridAttackBehavior(_enemy, hybridAttackDataType));
                    }
                    else
                    {
                        _enemy.SetAttackBehaviorInternal(new MeleeAttackBehavior(_enemy));
                    }

                    break;
                case AttackType.Ranged:
                    _enemy.SetAttackBehaviorInternal(new RangedAttackBehavior(_enemy));
                    
                    break;
                case AttackType.Boss:
                    BossAttackData bossAttackDataType = _enemy.Data.BaseAttackData as BossAttackData;

                    if (bossAttackDataType != null)
                    {
                        _enemy.SetAttackBehaviorInternal(new BossAttackBehavior(_enemy, bossAttackDataType));
                    }
                    else
                    {
                        _enemy.SetAttackBehaviorInternal(new MeleeAttackBehavior(_enemy));
                    }

                    break;
                default:
                    _enemy.SetAttackBehaviorInternal(new MeleeAttackBehavior(_enemy));
                    
                    break;
            }
        }

        public IEnumerator AttackCoroutine()
        {
            float updateInterval = 0.1f;

            while (true)
            {
                if (_enemy.Health.IsDead)
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