using System;
using UnityEngine;
using EnemyComponents.EnemySettings.Effects;

namespace EnemyComponents.EnemySettings.EnemyAttackType
{
    [Serializable]
    public class MeleeEnemyAttackType : BaseEnemyAttackType
    {
        [SerializeField] private EffectData _attackEffect1;
        [SerializeField] private EffectData _attackEffect2;

        public override AttackType Type => AttackType.Melee;
        public EffectData[] AttackEffects => new[] { _attackEffect1, _attackEffect2 };
    }
}