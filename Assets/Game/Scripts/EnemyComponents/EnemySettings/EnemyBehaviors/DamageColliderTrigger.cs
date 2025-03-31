using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.DamageAppliers;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class DamageColliderTrigger : MonoBehaviour
    {
        private MeleeDamageZoneApplier _meleeDamageZoneApplier;
        private BossDamageZoneApplier _bossDamageZoneApplier;

        private void Awake()
        {
            _meleeDamageZoneApplier = GetComponentInParent<MeleeDamageZoneApplier>();
            _bossDamageZoneApplier = GetComponentInParent<BossDamageZoneApplier>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_meleeDamageZoneApplier != null)
            {
                _meleeDamageZoneApplier.DealDamageIfEnabled(other);
            }
            else if (_bossDamageZoneApplier != null)
            {
                _bossDamageZoneApplier.DealMeleeDamageIfEnabled(other);
            }
        }
    }
}