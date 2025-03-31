using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.ProjectileComponents;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackData
{
    [Serializable]
    public class HybridAttackData : IEnemyAttackData
    {
        [SerializeField, HideInInspector] private AttackType _attackType = AttackType.Hybrid;
        [SerializeField] private List<EffectData> _attackEffects = new List<EffectData>();
        [SerializeField] private BaseProjectile _projectilePrefab;
        [SerializeField] private float _meleeRange = 3f;
        [SerializeField] private float _rangedRange = 6f;
        [SerializeField] private float _reloadTimeProjectile = 3f;

        public AttackType AttackType => _attackType;
        public List<EffectData> AttackEffects => _attackEffects;
        public BaseProjectile ProjectilePrefab => _projectilePrefab;
        public float MeleeRange => _meleeRange;
        public float RangedRange => _rangedRange;
        public float ReloadTimeProjectile => _reloadTimeProjectile;
    }
}