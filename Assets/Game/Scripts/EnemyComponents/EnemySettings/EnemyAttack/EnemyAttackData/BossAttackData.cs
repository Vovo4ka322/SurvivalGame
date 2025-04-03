using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackData
{
    [Serializable]
    public class BossAttackData : IEnemyAttackData
    {
        [SerializeField] [HideInInspector] private AttackType _attackType = AttackType.Boss;
        [SerializeField] private List<EffectData> _attackEffects = new List<EffectData>();
        [SerializeField] private float _meleeRange = 3f;

        public AttackType AttackType => _attackType;
        public List<EffectData> AttackEffects => _attackEffects;
        public float MeleeRange => _meleeRange;
    }
}