using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.PoolComponents;
using UnityEngine;

namespace Game.Scripts.EnemyComponents.EnemySettings.Effects
{
    public class EnemyEffects : MonoBehaviour, IEnemyEffects
    {
        private BaseEffectsEnemy _spawnEffect;
        private BaseEffectsEnemy _hitEffect;
        private BaseEffectsEnemy _deathEffect;
        private BaseEffectsEnemy _reloadEffect;
        private AttackEffect _attackEffect;
        private ICoroutineRunner _coroutineRunner;

        private readonly int _maxCountEffectsHybrid = 3;
        private readonly int _maxCountEffectsBoss = 4;

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

            switch (data.BaseAttackType.Type)
            {
                case AttackType.Melee:
                    MeleeAttack melee = data.BaseAttackType as MeleeAttack;

                    if (melee?.AttackEffects != null)
                    {
                        _attackEffect = new AttackEffect(_coroutineRunner, melee.AttackEffects, pool);
                    }

                    break;

                case AttackType.Ranged:
                    RangeAttack ranged = data.BaseAttackType as RangeAttack;

                    if (ranged?.ReloadEffect != null)
                    {
                        _reloadEffect = new BaseEffectsEnemy(_coroutineRunner, ranged.ReloadEffect, pool);
                    }

                    break;

                case AttackType.Hybrid:
                    HybridAttack hybrid = data.BaseAttackType as HybridAttack;

                    if (hybrid != null)
                    {
                        if (hybrid.AttackEffects != null && hybrid.AttackEffects.Length >= _maxCountEffectsHybrid)
                        {
                            EffectData[] attackEffects = { hybrid.AttackEffects[0], hybrid.AttackEffects[1], hybrid.AttackEffects[2] };

                            _attackEffect = new AttackEffect(_coroutineRunner, attackEffects, pool);
                            _reloadEffect = new BaseEffectsEnemy(_coroutineRunner, hybrid.AttackEffects[3], pool);
                        }
                    }

                    break;

                case AttackType.Boss:
                    BossAttack boss = data.BaseAttackType as BossAttack;

                    if (boss != null)
                    {
                        if (boss.AttackEffects != null && boss.AttackEffects.Length >= _maxCountEffectsBoss)
                        {
                            EffectData[] attackEffects =
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