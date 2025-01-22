using System;
using System.Collections.Generic;
using EnemyComponents.EnemySettings;
using UnityEngine;

namespace EnemyComponents.Animations
{
    public class EnemyAnimationController
    {
        private readonly Animator _animator;
        private readonly EnemyType _enemyType;
        
        private bool _isAttacking = false;
        private Dictionary<EnemyType, Dictionary<int, int>> _attackMappings = new()
        {
            {
                EnemyType.Easy, new Dictionary<int, int>
                {
                    { 1, AnimationDataParamsEnemy.Params.AttackVar1 },
                    { 2, AnimationDataParamsEnemy.Params.AttackVar2 }
                }
            },
            {
                EnemyType.Medium, new Dictionary<int, int>
                {
                    { 1, AnimationDataParamsEnemy.Params.AttackVar1 },
                    { 2, AnimationDataParamsEnemy.Params.AttackVar2 }
                }
            },
            {
                EnemyType.Hard, new Dictionary<int, int>
                {
                    { 1, AnimationDataParamsEnemy.Params.AttackVar1 },
                    { 2, AnimationDataParamsEnemy.Params.AttackVar2 },
                    { 3, AnimationDataParamsEnemy.Params.AttackAroundVariant1 },
                    { 4, AnimationDataParamsEnemy.Params.AttackAroundVariant2 },
                    { 5, AnimationDataParamsEnemy.Params.AttackProjectile }
                }
            },
            {
                EnemyType.Boss, new Dictionary<int, int>
                {
                    { 1, AnimationDataParamsEnemy.Params.AttackFrontVariant1 },
                    { 2, AnimationDataParamsEnemy.Params.AttackFrontVariant2 },
                    { 3, AnimationDataParamsEnemy.Params.AttackGround },
                    { 4, AnimationDataParamsEnemy.Params.AttackJump }
                }
            }
        };
        
        public EnemyAnimationController(Animator animator, EnemyType enemyType)
        {
            _animator = animator;
            _enemyType = enemyType;
        }
        
        public bool IsAttacking => _isAttacking;
        public int AttackVariantsCount => _attackMappings[_enemyType].Count;
        
        public void Spawn()
        {
            _animator.SetTrigger(AnimationDataParamsEnemy.Params.Spawning);
        }
        
        public void Move(bool isMoving)
        {
            _animator.SetBool(AnimationDataParamsEnemy.Params.Walking, isMoving);
        }
        
        public void TakeHit()
        {
            _animator.SetTrigger(AnimationDataParamsEnemy.Params.TakeDamage);
        }
        
        public void Death()
        {
            _animator.SetTrigger(AnimationDataParamsEnemy.Params.Dead);
        }
        
        public void Attack(int attackVariant)
        {
            if(_isAttacking)
            {
                return;
            }
            
            _isAttacking = true;
            
            if (_attackMappings.TryGetValue(_enemyType, out var attackMap) && attackMap.TryGetValue(attackVariant, out var triggerHash))
            {
                _animator.SetTrigger(triggerHash);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        
        public void ResetAttackState()
        {
            _isAttacking = false;
        }
    }
}