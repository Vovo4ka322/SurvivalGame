using UnityEngine;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class MeleeDamageArea : BaseDamageArea
    {
        [SerializeField] private Collider _damageCollider;

        private void Awake()
        {
            _damageCollider.enabled = false;
        }
        
        public void EnableDamageCollider()
        {
            _damageCollider.enabled = true;
        }
        
        public void DisableDamageCollider()
        {
            _damageCollider.enabled = false;
        }
        
        public void DealDamageIfEnabled(Collider enemyCollider)
        {
            if(!_damageCollider.enabled)
            {
                return;
            }
        
            DealDamageToCollider(enemyCollider);
        }
    }
}