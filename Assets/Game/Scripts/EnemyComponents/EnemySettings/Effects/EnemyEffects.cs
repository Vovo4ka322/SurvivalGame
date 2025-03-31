using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackData;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.PoolComponents;

namespace Game.Scripts.EnemyComponents.EnemySettings.Effects
{
    public class EnemyEffects : MonoBehaviour, IEnemyEffects
    {
        private readonly int _maxCountEffectsHybrid = 3;
        private readonly int _maxCountEffectsBoss = 4;
        
        private BaseEffectsEnemy _spawnEffect;
        private BaseEffectsEnemy _hitEffect;
        private BaseEffectsEnemy _deathEffect;
        private BaseEffectsEnemy _reloadEffect;
        private AttackEffect _attackEffect;
        private ICoroutineRunner _coroutineRunner;

        public void Initialize(EnemyData data, EffectsPool pool, ICoroutineRunner coroutineRunner)
        {
            if (data == null)
            {
                return;
            }

            _coroutineRunner = coroutineRunner;
            _spawnEffect = new BaseEffectsEnemy(_coroutineRunner, data.SpawnEffect, pool);
            _hitEffect = new BaseEffectsEnemy(_coroutineRunner, data.HitEffect, pool);
            _deathEffect = new BaseEffectsEnemy(_coroutineRunner, data.DeathEffect, pool);

            switch (data.BaseAttackData.AttackType)
            {
                case AttackType.Melee:
                    MeleeAttackData melee = data.BaseAttackData as MeleeAttackData;

                    if (melee?.AttackEffects != null)
                    {
                        _attackEffect = new AttackEffect(_coroutineRunner, melee.AttackEffects, pool);
                    }

                    break;
                case AttackType.Ranged:
                    RangeAttackData ranged = data.BaseAttackData as RangeAttackData;

                    if (ranged?.ReloadEffect != null)
                    {
                        _reloadEffect = new BaseEffectsEnemy(_coroutineRunner, ranged.ReloadEffect, pool);
                    }

                    break;
                case AttackType.Hybrid:
                    HybridAttackData hybrid = data.BaseAttackData as HybridAttackData;

                    if (hybrid != null)
                    {
                        if (hybrid.AttackEffects != null && hybrid.AttackEffects.Count >= _maxCountEffectsHybrid)
                        {
                            List<EffectData> attackEffects = new List<EffectData>
                            {
                                hybrid.AttackEffects[0],
                                hybrid.AttackEffects[1],
                            };
                            
                            _attackEffect = new AttackEffect(_coroutineRunner, attackEffects, pool);
                            _reloadEffect = new BaseEffectsEnemy(_coroutineRunner, hybrid.AttackEffects[2], pool);
                        }
                    }

                    break;
                case AttackType.Boss:
                    BossAttackData boss = data.BaseAttackData as BossAttackData;

                    if (boss != null)
                    {
                        if (boss.AttackEffects != null && boss.AttackEffects.Count >= _maxCountEffectsBoss)
                        {
                            List<EffectData> attackEffects = new List<EffectData>
                            {
                                boss.AttackEffects[0],
                                boss.AttackEffects[1],
                                boss.AttackEffects[2],
                                boss.AttackEffects[3]
                            };

                            _attackEffect = new AttackEffect(_coroutineRunner, attackEffects, pool);
                        }
                    }

                    break;
            }
        }

        public void Spawn() => _spawnEffect?.Play(transform);

        public void StopSpawn() => _spawnEffect?.Stop();

        public void Hit() => _hitEffect?.Play(transform);

        public void Death() => _deathEffect?.Play(transform);

        public void Reload() => _reloadEffect?.Play(transform);

        public void StopReload() => _reloadEffect?.Stop();

        public void Attack(int numberEffect) => _attackEffect?.Attack(numberEffect, transform);
    }
}