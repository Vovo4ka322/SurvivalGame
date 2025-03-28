using UnityEngine;
using Game.Scripts.EnemyComponents.Animations;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class EnemyAttack : IEnemyAttack
    {
        private readonly Enemy _enemy;
        private readonly EnemyAnimationState _animationState;
        private readonly BaseEnemyAttackType _attackType;
        private readonly Transform _enemyTransform;
        private readonly Player _player;

        private readonly float _attackCooldown;
        private readonly int _attackVariants;

        private int _lastAttackVariant = 0;
        private float _lastAttackTime;
        private float _lastRangedAttackTime = -Mathf.Infinity;

        public EnemyAttack(EnemyAnimationState animationState, Transform enemyTransform, Player player, float attackCooldown, BaseEnemyAttackType attackType, int attackVariants)
        {
            _animationState = animationState;
            _enemyTransform = enemyTransform;
            _player = player;
            _attackCooldown = attackCooldown;
            _attackType = attackType;
            _attackVariants = attackVariants;
            _enemy = enemyTransform.GetComponent<Enemy>();
        }

        public void TryAttack()
        {
            if (_enemy.Health.IsDead)
            {
                return;
            }

            if (_animationState.IsAttacking)
            {
                return;
            }

            Vector3 diff = _enemyTransform.position - _player.transform.position;
            float sqrDistance = diff.sqrMagnitude;

            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                if (_attackType.Type == AttackType.Melee)
                {
                    int attackVariant = GetAttackVariant();
                    _lastAttackTime = Time.time;
                    PerformMeleeAttack(attackVariant);
                }
                else if (_attackType.Type == AttackType.Ranged)
                {
                    int attackVariant = GetAttackVariant();
                    _lastAttackTime = Time.time;
                    PerformRangedAttack(attackVariant);
                }
                else if (_attackType.Type == AttackType.Hybrid)
                {
                    HybridEnemyAttackType hybridType = (HybridEnemyAttackType)_attackType;

                    if (sqrDistance <= hybridType.MeleeRange * hybridType.MeleeRange)
                    {
                        _lastAttackTime = Time.time;
                        int meleeVariant = Random.Range(3, 5);
                        PerformMeleeAttack(meleeVariant);
                    }
                    else if (sqrDistance <= hybridType.RangedRange * hybridType.RangedRange)
                    {
                        if (Time.time >= _lastRangedAttackTime + hybridType.ReloadTimeProjectile)
                        {
                            _lastRangedAttackTime = Time.time;
                            int rangedAttackVariant = 5;
                            _lastAttackTime = Time.time;
                            PerformRangedAttack(rangedAttackVariant);
                        }
                    }
                }
                else if (_attackType.Type == AttackType.Boss)
                {
                    BossEnemyAttackType bossType = (BossEnemyAttackType)_attackType;

                    if (sqrDistance <= bossType.MeleeRange * bossType.MeleeRange)
                    {
                        _lastAttackTime = Time.time;
                        int meleeVariant = Random.Range(1, 5);
                        PerformMeleeAttack(meleeVariant);
                    }
                }
            }
        }

        public bool IsHybridProjectileReady(HybridEnemyAttackType hybridType, float distance)
        {
            if (_attackType.Type == AttackType.Hybrid)
            {
                return distance <= hybridType.RangedRange && distance > hybridType.MeleeRange && Time.time >= _lastRangedAttackTime + hybridType.ReloadTimeProjectile;
            }

            return false;
        }

        private void PerformMeleeAttack(int attackVariant)
        {
            if (_enemy.Data.EnemyType == EnemyType.Boss && attackVariant == 2)
            {
                _enemy.LockTargetPosition(_player.transform.position);
            }

            _animationState.Attack(attackVariant);
        }

        private void PerformRangedAttack(int attackVariant)
        {
            _animationState.Attack(attackVariant);
        }

        private int GetAttackVariant()
        {
            if (_attackType.Type == AttackType.Melee)
            {
                int meleeVariants = 2;

                if (meleeVariants <= 1)
                {
                    _lastAttackVariant = 1;

                    return 1;
                }

                if (_lastAttackVariant < 1 || _lastAttackVariant > meleeVariants)
                {
                    _lastAttackVariant = Random.Range(1, meleeVariants + 1);

                    return _lastAttackVariant;
                }

                int randomIndex = Random.Range(0, meleeVariants - 1);
                int newVariant = randomIndex + 1;

                if (newVariant >= _lastAttackVariant)
                {
                    newVariant++;
                }

                _lastAttackVariant = newVariant;

                return newVariant;
            }
            else
            {
                if (_attackVariants <= 1)
                {
                    _lastAttackVariant = 1;

                    return 1;
                }

                if (_lastAttackVariant < 1 || _lastAttackVariant > _attackVariants)
                {
                    _lastAttackVariant = Random.Range(1, _attackVariants + 1);

                    return _lastAttackVariant;
                }

                int randomIndex = Random.Range(0, _attackVariants - 1);
                int newVariant = randomIndex + 1;

                if (newVariant >= _lastAttackVariant)
                {
                    newVariant++;
                }

                _lastAttackVariant = newVariant;

                return newVariant;
            }
        }
    }
}
