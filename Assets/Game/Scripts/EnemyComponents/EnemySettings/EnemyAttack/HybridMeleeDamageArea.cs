using UnityEngine;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class HybridMeleeDamageArea : BaseDamageArea
    {
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _damageRadius = 3f;

        private readonly Collider[] _areaBuffer = new Collider[10];

        public void DealAreaDamage()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(_attackPoint.position, _damageRadius, _areaBuffer, _targetLayer);

            for (int i = 0; i < hitCount; i++)
            {
                DealDamageToCollider(_areaBuffer[i]);
            }

            Enemy?.SoundCollection?.HybridSoundEffects?.PlayMeleeAttack();
        }
    }
}