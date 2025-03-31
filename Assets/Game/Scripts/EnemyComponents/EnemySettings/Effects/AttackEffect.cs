using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.PoolComponents;

namespace Game.Scripts.EnemyComponents.EnemySettings.Effects
{
    public class AttackEffect
    {
        private readonly BaseEffectsEnemy[] _effects;

        public AttackEffect(ICoroutineRunner coroutineRunner, IEnumerable<EffectData> attackEffects, EffectsPool pool)
        {
            if (attackEffects == null)
            {
                _effects = Array.Empty<BaseEffectsEnemy>();

                return;
            }

            List<EffectData> effectsList = attackEffects.ToList();
            
            if (effectsList.Count == 0)
            {
                _effects = Array.Empty<BaseEffectsEnemy>();
                return;
            }
            
            _effects = new BaseEffectsEnemy[effectsList.Count];
            
            for (int i = 0; i < effectsList.Count; i++)
            {
                _effects[i] = new BaseEffectsEnemy(coroutineRunner, effectsList[i], pool);
            }
        }

        public void Attack(int numberEffect, Transform ownerTransform)
        {
            if (numberEffect < 0 || numberEffect >= _effects.Length)
            {
                return;
            }

            _effects[numberEffect].Play(ownerTransform);
        }
    }
}