using System;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using Game.Scripts.EnemyComponents.Projectiles;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttackType
{
    [Serializable]
    public class HybridEnemyAttackType : BaseEnemyAttackType
    {
        [SerializeField] private EffectData _attackEffect1;
        [SerializeField] private EffectData _attackEffect2;
        [SerializeField] private EffectData _attackEffect3;
        [SerializeField] private EffectData _reloadEffect;
        [SerializeField] private BaseProjectile _projectilePrefab;
        [SerializeField] private float _meleeRange = 3f;
        [SerializeField] private float _rangedRange = 6f;
        [SerializeField] private float _reloadTimeProjectile = 3f;
        
        public override AttackType Type => AttackType.Hybrid;
        public EffectData[] AttackEffects => new[] { _attackEffect1, _attackEffect2, _attackEffect3, _reloadEffect };
        public BaseProjectile ProjectilePrefab => _projectilePrefab;
        public float MeleeRange => _meleeRange;
        public float RangedRange => _rangedRange;
        public float ReloadTimeProjectile => _reloadTimeProjectile;
    }
}