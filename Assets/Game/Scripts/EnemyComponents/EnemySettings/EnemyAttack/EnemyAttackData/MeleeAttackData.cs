using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackData
{
    [Serializable]
    public class MeleeAttackData : IEnemyAttackData
    {
        [SerializeField] private List<EffectData> _attackEffects = new List<EffectData>();

        public AttackType AttackType => AttackType.Melee;
        public List<EffectData> AttackEffects => _attackEffects;
    }
}