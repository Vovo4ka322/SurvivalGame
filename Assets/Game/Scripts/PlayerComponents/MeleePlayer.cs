using UnityEngine;
using Ability.MeleeAbilities.Bloodlust;
using Game.Scripts.EnemyComponents;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents.Animations;
using Weapons.MeleeWeapon;

namespace Game.Scripts.PlayerComponents
{
    public class MeleePlayer : Player, IActivable
    {
        [SerializeField] private Sword _sword;

        private float _movementVisualizationCoefficient = 0.2f;
        private float _attackSpeed;
        private float _damage;

        public bool IsActiveState { get; private set; }

        private void Start()
        {
            ChangeAttackAnimationSpeed(AnimatorState.Speed, GeneralAttackSpeed);
            _damage = GeneralDamage + _sword.WeaponData.Damage;
            SetTotalDamage(_damage);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (IsActiveState)
                {
                    AddHealth(enemy.SetDamage());
                }
                else
                {
                    LoseHealth(enemy.SetDamage());
                }

                if (Health.IsDead)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void UpgradeCharacteristikByBloodlust(Bloodlust bloodlust)
        {
            PlayerMovement.ChangeMoveSpeed(bloodlust.MovementSpeed);
            _attackSpeed = bloodlust.AttackSpeed + GeneralAttackSpeed;

            ChangeAttackAnimationSpeed(AnimatorState.Speed, _attackSpeed);
            ChangeMovementAnimationSpeed(AnimatorState.MovementSpeed, _movementVisualizationCoefficient);
            _movementVisualizationCoefficient += _movementVisualizationCoefficient;
        }

        public bool SetTrueActiveState() => IsActiveState = true;

        public bool SetFalseActiveState() => IsActiveState = false;

        public void SetSwordColliderTrue() => _sword.MeshCollider.enabled = true;

        public void SetSwordColliderFalse() => _sword.MeshCollider.enabled = false;
    }
}