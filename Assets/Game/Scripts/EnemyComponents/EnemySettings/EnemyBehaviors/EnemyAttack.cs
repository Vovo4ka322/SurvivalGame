using UnityEngine;
using PlayerComponents;
using EnemyComponents.Animations;
using EnemyComponents.EnemySettings.EnemyAttackType;
using EnemyComponents.Interfaces;

namespace EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class EnemyAttack : IEnemyAttack
    {
        private readonly EnemyAnimationController _animationController;
        private readonly BaseEnemyAttackType _attackType;
        private readonly Transform _enemyTransform;
        private readonly Player _player;
        
        private readonly float _attackCooldown;
        private readonly int _attackVariants;
        
        private int _lastAttackVariant = 0;
        private float _lastAttackTime;
        private float _lastRangedAttackTime = -Mathf.Infinity;

        public EnemyAttack(EnemyAnimationController animationController, Transform enemyTransform, Player player, float attackCooldown, BaseEnemyAttackType attackType, int attackVariants)
        {
            _animationController = animationController;
            _enemyTransform = enemyTransform;
            _player = player;
            _attackCooldown = attackCooldown;
            _attackType = attackType;
            _attackVariants = attackVariants;
        }
        
        public void TryAttack()
        {
            if(_animationController.IsAttacking)
            {
                return;
            }
            
            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                int attackVariant = GetAttackVariant();

                if (_attackType.Type == AttackType.Melee)
                {
                    _lastAttackTime = Time.time;
                    PerformMeleeAttack(attackVariant);
                }
                else if (_attackType.Type == AttackType.Ranged)
                {
                    _lastAttackTime = Time.time;
                    PerformRangedAttack(attackVariant);
                }
                else if (_attackType.Type == AttackType.Hybrid)
                {
                    HybridEnemyAttackType hybridType = (HybridEnemyAttackType)_attackType;
                    float distance = Vector3.Distance(_enemyTransform.position, _player.transform.position);
                    
                    if(distance <= hybridType.MeleeRange)
                    {
                        _lastAttackTime = Time.time;
                        int meleeVariant = Random.Range(3, 5);
                        PerformMeleeAttack(meleeVariant);
                    }
                    else if(distance <= hybridType.RangedRange && distance > hybridType.MeleeRange)
                    {
                        if(Time.time >= _lastRangedAttackTime + hybridType.ReloadTimeProjectile)
                        {
                            _lastRangedAttackTime = Time.time;
                            int rangedAttackVariant = 5;
                            PerformRangedAttack(rangedAttackVariant);
                        }
                    }
                }
            }
        }
        
        public bool IsHybridProjectileReady(HybridEnemyAttackType hybridType, float distance)
        {
            if(_attackType.Type == AttackType.Hybrid)
            {
                return (distance <= hybridType.RangedRange && distance > hybridType.MeleeRange && Time.time >= _lastRangedAttackTime + hybridType.ReloadTimeProjectile);
            }
            
            return false;
        }

        private void PerformMeleeAttack(int attackVariant)
        {
            _animationController.Attack(attackVariant);
        }

        private void PerformRangedAttack(int attackVariant)
        {
            _animationController.Attack(attackVariant);
        }
        
        private int GetAttackVariant()
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