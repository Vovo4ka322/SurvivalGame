using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack;
using Game.Scripts.Weapons;
using Game.Scripts.Weapons.MeleeWeapon;
using UnityEngine;

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
            if (_enemy == null)
            {
                return;
            }

            Weapon weapon = other.gameObject.GetComponentInParent<Weapon>();

            if (weapon != null)
            {
                _enemy.ChangeHealth(weapon.TotalDamage);

                if (weapon is Sword sword)
                {
                    sword.RegisterHit();
                }

                return;
            }

            MeleeDamageArea meleeDamageArea = _enemy.GetComponentInChildren<MeleeDamageArea>();
            BossDamageArea bossDamageArea = _enemy.GetComponentInChildren<BossDamageArea>();

            if (meleeDamageArea != null)
            {
                meleeDamageArea.DealDamageIfEnabled(other);
            }
            else if (bossDamageArea != null)
            {
                bossDamageArea.DealMeleeDamageIfEnabled(other);
            }
        }
    }
}