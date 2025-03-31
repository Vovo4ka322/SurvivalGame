using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class DamageColliderTrigger : MonoBehaviour
    {
        private MeleeDamageArea _meleeDamageArea;
        private BossDamageArea _bossDamageArea;

        private void Awake()
        {
            _meleeDamageArea = GetComponentInParent<MeleeDamageArea>();
            _bossDamageArea = GetComponentInParent<BossDamageArea>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_meleeDamageArea != null)
            {
                _meleeDamageArea.DealDamageIfEnabled(other);
            }
            else if (_bossDamageArea != null)
            {
                _bossDamageArea.DealMeleeDamageIfEnabled(other);
            }
        }
    }
}