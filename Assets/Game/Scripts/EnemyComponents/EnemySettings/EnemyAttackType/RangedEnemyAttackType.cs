using System;
using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using Game.Scripts.EnemyComponents.Projectiles;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttackType
{
    [Serializable]
    public class RangedEnemyAttackType : BaseEnemyAttackType
    {
        [SerializeField] private EffectData _reloadEffect;
        [SerializeField] private BaseProjectile _projectilePrefab; 
        
        public override AttackType Type => AttackType.Ranged;
        public EffectData ReloadEffect => _reloadEffect;
        public BaseProjectile ProjectilePrefab => _projectilePrefab;
    }
}