using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.DamageAppliers;
using Game.Scripts.Weapons;
using Game.Scripts.Weapons.MeleeWeapon;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
{
    [RequireComponent(typeof(Collider))]
    public class EnemyPhysics : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        private MeleeDamageZoneApplier _damageZoneApplier;
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

            MeleeDamageZoneApplier meleeDamageZoneApplier = _enemy.GetComponentInChildren<MeleeDamageZoneApplier>();
            BossDamageZoneApplier bossDamageZoneApplier = _enemy.GetComponentInChildren<BossDamageZoneApplier>();

            if (meleeDamageZoneApplier != null)
            {
                meleeDamageZoneApplier.DealDamageIfEnabled(other);
            }
            else if (bossDamageZoneApplier != null)
            {
                bossDamageZoneApplier.DealMeleeDamageIfEnabled(other);
            }
        }
    }
}