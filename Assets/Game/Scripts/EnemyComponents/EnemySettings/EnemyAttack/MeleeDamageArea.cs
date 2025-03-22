using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class MeleeDamageArea : BaseDamageArea
    {
        [SerializeField] private Collider _damageCollider;
        
        private bool _hasHit = false;
        
        private void Awake()
        {
            _damageCollider.enabled = false;
        }
        
        public void EnableDamageCollider()
        {
            _hasHit = false;
            _damageCollider.enabled = true;
        }
        
        public void DisableDamageCollider()
        {
            _damageCollider.enabled = false;
            
            if (!_hasHit)
            {
                Enemy?.SoundCollection?.MeleeSoundEffects?.PlayMissHit();
            }
        }
        
        public void DealDamageIfEnabled(Collider enemyCollider)
        {
            if(!_damageCollider.enabled)
            {
                return;
            }
        
            if (enemyCollider.TryGetComponent(out IDamagable _))
            {
                _hasHit = true;
                
                DealDamageToCollider(enemyCollider);
            
                Enemy?.SoundCollection?.MeleeSoundEffects?.PlayHit();
            }
        }
    }
}