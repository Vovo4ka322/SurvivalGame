using System;
using EnemyComponents.Interfaces;
using UnityEngine;
using Pools;

namespace EnemyComponents.EnemySettings.Effects
{
    public class AttackEffect
    {
        private readonly BaseEffectsEnemy[] _effects;
        
        public AttackEffect(ICoroutineRunner coroutineRunner, EffectData[] attackEffects, EffectsPool pool)
        {
            if(attackEffects == null || attackEffects.Length == 0) 
            {
                _effects = Array.Empty<BaseEffectsEnemy>();
                
                return;
            }
            
            _effects = new BaseEffectsEnemy[attackEffects.Length];
            
            for(int i = 0; i < attackEffects.Length; i++)
            {
                _effects[i] = new SimpleEffect(coroutineRunner, attackEffects[i], pool);
            }
        }
        
        public void Attack(int numberEffect, Transform ownerTransform)
        {
            if(numberEffect < 0 || numberEffect >= _effects.Length)
            {
                return;
            }
            
            _effects[numberEffect].Play(ownerTransform);
        }
    }
}