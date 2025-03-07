using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack;
using Game.Scripts.PlayerComponents;
using Weapons;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
{
    [RequireComponent(typeof(Collider))]
    public class EnemyPhysics : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        
        private MeleeDamageArea _damageArea;
        private Collider _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_enemy == null)
            {
                return;
            }
            
            if (other.gameObject.TryGetComponent(out Weapon weapon))
            {
                _enemy.Health.Lose(weapon.TotalDamage);
                
                return;
            }

            MeleeDamageArea meleeDamageArea = _enemy.GetComponentInChildren<MeleeDamageArea>();
            BossDamageArea bossDamageArea = _enemy.GetComponentInChildren<BossDamageArea>();
            
            if (meleeDamageArea != null || bossDamageArea != null)
            {
                meleeDamageArea.DealDamageIfEnabled(other);
                bossDamageArea.DealMeleeDamageIfEnabled(other);
            }
        }
    }
}