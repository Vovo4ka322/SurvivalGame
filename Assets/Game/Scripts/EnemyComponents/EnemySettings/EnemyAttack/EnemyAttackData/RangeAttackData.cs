using System;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.ProjectileComponents;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackData
{
    [Serializable]
    public class RangeAttackData : IEnemyAttackData
    {
        [SerializeField, HideInInspector] private AttackType _attackType = AttackType.Ranged;
        [SerializeField] private EffectData _reloadEffect;
        [SerializeField] private BaseProjectile _projectilePrefab;

        public AttackType AttackType => _attackType;
        public EffectData ReloadEffect => _reloadEffect;
        public BaseProjectile ProjectilePrefab => _projectilePrefab;
    }
}