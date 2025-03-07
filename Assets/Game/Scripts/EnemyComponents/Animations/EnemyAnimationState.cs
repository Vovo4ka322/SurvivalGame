using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.Animations
{
    public class EnemyAnimationState : IEnemyAnimation
    {
        private readonly Animator _animator;
        private readonly EnemyType _enemyType;
        private readonly Dictionary<EnemyType, Dictionary<int, int>> _attackMappings;
        
        private bool _isAttacking = false;
        
        public EnemyAnimationState(Animator animator, EnemyType enemyType)
        {
            _animator = animator;
            _enemyType = enemyType;
            
            _attackMappings = new()
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
        }
        
        public bool IsAttacking => _isAttacking;
        public int AttackVariantsCount => _attackMappings[_enemyType].Count;
        
        public void Spawn()
        {
            _animator.SetTrigger(AnimationDataParamsEnemy.Params.Spawning);
        }
        
        public void Move(bool isMoving)
        {
            if(ParameterExists(AnimationDataParamsEnemy.Params.Walking))
            {
                _animator.SetBool(AnimationDataParamsEnemy.Params.Walking, isMoving);
            }
        }
        
        public void TakeHit()
        {
            if(ParameterExists(AnimationDataParamsEnemy.Params.TakeDamage))
            {
                _animator.SetTrigger(AnimationDataParamsEnemy.Params.TakeDamage);
            }
        }
        
        public void Death()
        {
            if(ParameterExists(AnimationDataParamsEnemy.Params.Walking))
            {
                _animator.SetBool(AnimationDataParamsEnemy.Params.Walking, false);
            }
            
            if (_attackMappings.TryGetValue(_enemyType, out var attackMap))
            {
                foreach (var triggerHash in attackMap.Values)
                {
                    if (ParameterExists(triggerHash))
                    {
                        _animator.ResetTrigger(triggerHash);
                    }
                }
            }
            
            _animator.CrossFade("Dead", 0.1f);
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
                throw new ArgumentException("The wrong attack option");
            }
        }
        
        public void ResetAttackState()
        {
            _isAttacking = false;
        }
        
        private bool ParameterExists(int hash)
        {
            foreach (var parameter in _animator.parameters)
            {
                if (parameter.nameHash == hash)
                    return true;
            }
            return false;
        }
    }
}