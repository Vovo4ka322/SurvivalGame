using System;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType
{
    [Serializable]
    public class MeleeEnemyAttackSetter : EnemyAttackTypeSetter
    {
        [SerializeField] private EffectData _attackEffect1;
        [SerializeField] private EffectData _attackEffect2;

        public override AttackType Type => AttackType.Melee;
        public EffectData[] AttackEffects => new[] { _attackEffect1, _attackEffect2 };
    }
}