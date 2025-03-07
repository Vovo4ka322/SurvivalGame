using System;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using UnityEngine;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType
{
    [Serializable]
    public class BossEnemyAttackType : BaseEnemyAttackType
    {
        [SerializeField] private EffectData _attackEffect1;
        [SerializeField] private EffectData _attackEffect2;
        [SerializeField] private EffectData _attackEffect3;
        [SerializeField] private EffectData _attackEffect4;
        [SerializeField] private float _meleeRange = 3f;
        [SerializeField] private float _rangedRange = 6f;
        
        public override AttackType Type => AttackType.Boss;
        public EffectData[] AttackEffects => new[] { _attackEffect1, _attackEffect2, _attackEffect3, _attackEffect4};
        public float MeleeRange => _meleeRange;
        public float RangedRange => _rangedRange;
    }
}