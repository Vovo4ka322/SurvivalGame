using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class DamageColliderTrigger : MonoBehaviour
    {
        private MeleeDamageArea _meleeDamageArea;

        private void Awake()
        {
            _meleeDamageArea = GetComponentInParent<MeleeDamageArea>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_meleeDamageArea != null)
            {
                _meleeDamageArea.DealDamageIfEnabled(other);
            }
        }
    }
}