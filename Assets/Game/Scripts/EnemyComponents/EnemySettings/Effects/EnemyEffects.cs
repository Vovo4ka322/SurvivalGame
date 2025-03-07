using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.PoolComponents;

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
            if(data == null)
            {
                return;
            }
            
            _coroutineRunner = coroutineRunner;
            _spawnEffect = new SimpleEffect(_coroutineRunner, data.SpawnEffect, pool);
            _hitEffect = new SimpleEffect(_coroutineRunner, data.HitEffect, pool);
            _deathEffect = new SimpleEffect(_coroutineRunner, data.DeathEffect, pool);
            
            switch(data.BaseAttackType.Type)
            {
                case AttackType.Melee:
                    MeleeEnemyAttackType melee = data.BaseAttackType as MeleeEnemyAttackType;
                    
                    if(melee?.AttackEffects != null)
                    {
                        _attackEffect = new AttackEffect(_coroutineRunner, melee.AttackEffects, pool);
                    }
                    
                    break;
                
                case AttackType.Ranged:
                    RangedEnemyAttackType ranged = data.BaseAttackType as RangedEnemyAttackType;
                    
                    if(ranged?.ReloadEffect != null)
                    {
                        _reloadEffect = new SimpleEffect(_coroutineRunner, ranged.ReloadEffect, pool);
                    }
                    
                    break;
                
                case AttackType.Hybrid:
                    HybridEnemyAttackType hybrid = data.BaseAttackType as HybridEnemyAttackType;
                    
                    if(hybrid != null)
                    {
                        if(hybrid.AttackEffects != null && hybrid.AttackEffects.Length >= _maxCountEffectsHybrid)
                        {
                            EffectData[] attackEffects = { hybrid.AttackEffects[0], hybrid.AttackEffects[1], hybrid.AttackEffects[2] };
                            
                            _attackEffect = new AttackEffect(_coroutineRunner, attackEffects, pool);
                            _reloadEffect = new SimpleEffect(_coroutineRunner, hybrid.AttackEffects[3], pool);
                        }
                    }
                    
                    break;
                
                case AttackType.Boss:
                    BossEnemyAttackType boss = data.BaseAttackType as BossEnemyAttackType;
                    
                    if(boss != null)
                    {
                        if(boss.AttackEffects != null && boss.AttackEffects.Length >= _maxCountEffectsBoss)
                        {
                            EffectData[] attackEffects = {boss.AttackEffects[0], boss.AttackEffects[1], boss.AttackEffects[2], boss.AttackEffects[3]};
                            
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