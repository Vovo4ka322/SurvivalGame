using UnityEngine;
using Game.Scripts.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.DamageAppliers
{
    public class BossDamageZoneApplier : BaseDamageZoneApplier
    {
        private readonly Collider[] _radialBuffer1 = new Collider[10];
        private readonly Collider[] _radialBuffer2 = new Collider[10];
        private readonly Collider[] _directBuffer = new Collider[10];
        
        [Header("Radius attacks")]
        [SerializeField] private Transform _areaAttackPoint1;
        [SerializeField] private Transform _areaAttackPoint2;
        [SerializeField] private LayerMask _areaTargetLayer;
        [SerializeField] private float _areaDamageRadius1 = 3f;
        [SerializeField] private float _areaDamageRadius2 = 3f;

        [Header("Middle battle attack")]
        [SerializeField] private Collider _meleeDamageCollider;

        [Header("Direct attack")]
        [SerializeField] private Transform _directAttackPoint;
        [SerializeField] private Vector3 _directAttackBoxSize = new Vector3(3f, 2f, 5f);
        [SerializeField] private LayerMask _directTargetLayer;
        
        private bool _hasHit = false;
        
        public void DealRadialDamage1()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(_areaAttackPoint1.position, _areaDamageRadius1, _radialBuffer1, _areaTargetLayer);

            for (int i = 0; i < hitCount; i++)
            {
                DealDamageToCollider(_radialBuffer1[i]);
            }

            Enemy?.SoundCollection?.BossSoundEffects?.PlayMeleeAttack1();
        }

        public void DealRadialDamage2()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(_areaAttackPoint2.position, _areaDamageRadius2, _radialBuffer2, _areaTargetLayer);

            for (int i = 0; i < hitCount; i++)
            {
                DealDamageToCollider(_radialBuffer2[i]);
            }

            Enemy?.SoundCollection?.BossSoundEffects?.PlayMeleeAttack1();
        }

        public void EnableMeleeDamageCollider()
        {
            _hasHit = false;
            _meleeDamageCollider.enabled = true;
        }

        public void DisableMeleeDamageCollider()
        {
            _meleeDamageCollider.enabled = false;

            if (!_hasHit)
            {
                Enemy?.SoundCollection?.BossSoundEffects?.PlayMeleeMissHit();
            }
        }

        public void DealMeleeDamageIfEnabled(Collider other)
        {
            if (_meleeDamageCollider == null || !_meleeDamageCollider.enabled)
            {
                return;
            }

            if (other.TryGetComponent(out IDamagable _))
            {
                _hasHit = true;

                DealDamageToCollider(other);

                Enemy?.SoundCollection?.BossSoundEffects?.PlayMeleeHit();
            }
        }

        public void DealDirectDamage()
        {
            int hitCount = Physics.OverlapBoxNonAlloc(_directAttackPoint.position, _directAttackBoxSize * 0.5f, _directBuffer, _directAttackPoint.rotation, _directTargetLayer);

            for (int i = 0; i < hitCount; i++)
            {
                DealDamageToCollider(_directBuffer[i]);
            }

            Enemy?.SoundCollection?.BossSoundEffects?.PlayMeleeAttack2();
        }
    }
}