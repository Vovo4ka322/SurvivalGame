using System;
using UnityEngine;
using Pools;

namespace EnemyComponents.EnemySettings.Effects
{
    public class AttackEffect
    {
        private readonly BaseEffectsEnemy[] _effects;
        
        public AttackEffect(MonoBehaviour owner, EffectData[] attackEffects, PoolSettings poolSettings)
        {
            if(attackEffects == null) 
            {
                _effects = Array.Empty<BaseEffectsEnemy>();
                
                return;
            }
            
            _effects = new BaseEffectsEnemy[attackEffects.Length];
            
            for(int i = 0; i < attackEffects.Length; i++)
            {
                _effects[i] = new SimpleEffect(owner, attackEffects[i], poolSettings);
            }
        }
        
        public void Attack(int numberEffect)
        {
            if(numberEffect < 0 || numberEffect >= _effects.Length)
            {
                return;
            }
            
            _effects[numberEffect].Play();
        }
    }
}