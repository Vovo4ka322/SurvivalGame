using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack;
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

                if (_enemy.Health.IsDead)
                {
                    _enemy.PlayerTransform.GetExperience(_enemy.Data.Experience);
                    _enemy.PlayerTransform.GetMoney(_enemy.Data.Money);
                }
                
                return;
            }

            MeleeDamageArea meleeDamageArea = _enemy.GetComponentInChildren<MeleeDamageArea>();
            
            if (meleeDamageArea != null)
            {
                meleeDamageArea.DealDamageIfEnabled(other);
            }
        }
    }
}