using UnityEngine;
using EnemyComponents.EnemySettings.EnemyAttackType;
using Pools;

namespace EnemyComponents.EnemySettings.Effects
{
    public class EnemyEffects : MonoBehaviour
    {
        [SerializeField] private PoolSettings _particleEffectsPoolSettings;
        
        private BaseEffectsEnemy _spawnEffect;
        private BaseEffectsEnemy _hitEffect;
        private BaseEffectsEnemy _deathEffect;
        private BaseEffectsEnemy _reloadEffect;
        private AttackEffect _attackEffect;
        
        private readonly int _maxCountEffects = 3;
        
        public void InitializeParticle(EnemyData data)
        {
            if(data == null)
            {
                return;
            }
            
            _spawnEffect = new SimpleEffect(this, data.SpawnEffect, _particleEffectsPoolSettings);
            _hitEffect   = new SimpleEffect(this, data.HitEffect,   _particleEffectsPoolSettings);
            _deathEffect = new SimpleEffect(this, data.DeathEffect, _particleEffectsPoolSettings);
            
            switch(data.BaseAttackType.Type)
            {
                case AttackType.Melee:
                    MeleeEnemyAttackType melee = data.BaseAttackType as MeleeEnemyAttackType;
                    _attackEffect = new AttackEffect(this, melee?.AttackEffects, _particleEffectsPoolSettings);
                    break;
                
                case AttackType.Ranged:
                    RangedEnemyAttackType ranged = data.BaseAttackType as RangedEnemyAttackType;
                    _reloadEffect = new SimpleEffect(this, ranged?.ReloadEffect, _particleEffectsPoolSettings);
                    break;
                
                case AttackType.Hybrid:
                    HybridEnemyAttackType hybrid = data.BaseAttackType as HybridEnemyAttackType;
                    
                    if(hybrid != null && hybrid.AttackEffects != null && hybrid.AttackEffects.Length >= _maxCountEffects)
                    {
                        var attackEffects = new EffectData[]
                        {
                            hybrid.AttackEffects[0],
                            hybrid.AttackEffects[1],
                            hybrid.AttackEffects[2]
                        };
                        
                        _attackEffect = new AttackEffect(this, attackEffects, _particleEffectsPoolSettings);
                        _reloadEffect = new SimpleEffect(this, hybrid.AttackEffects[3], _particleEffectsPoolSettings);
                    }
                    
                    break;
            }
        }
        
        public void Spawn() => _spawnEffect?.Play();
        
        public void StopSpawn() => _spawnEffect?.Stop();
        
        public void Hit() => _hitEffect?.Play();
        
        public void Dead() => _deathEffect?.Play();
        
        public void Reload() => _reloadEffect?.Play();
        
        public void StopReload() => _reloadEffect?.Stop();
        
        public void Attack(int numberEffect) => _attackEffect?.Attack(numberEffect);
    }
}