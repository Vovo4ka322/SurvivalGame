using UnityEngine;
using Ability.MeleeAbilities.Bloodlust;
using Game.Scripts.EnemyComponents;
using Game.Scripts.Interfaces;
using Weapons.MeleeWeapon;

namespace Game.Scripts.PlayerComponents
{
    public class MeleePlayer : Player, IActivable, IDamagable
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
            _sword.SetTotalDamage(_damage);
        }

        public void TakeDamage(float value)
        {
            if (IsActiveState)
                AddHealth(value);
            else
                LoseHealth(value);

            if (Health.IsDead)
                Destroy(gameObject);
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