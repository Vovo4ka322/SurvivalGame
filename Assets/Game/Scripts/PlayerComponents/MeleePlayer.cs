using UnityEngine;
using Ability.MeleeAbilities.Bloodlust;
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
        private int _coefficient = 1;

        public bool IsActiveState { get; private set; }

        private void Start()
        {
            ChangeAttackAnimationSpeed(AnimatorState.Speed, GeneralAttackSpeed);
            _damage = GeneralDamage + _sword.WeaponData.Damage;
            _sword.SetTotalDamage(_damage);
            _sword.SetPlayer(this);
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
            ChangeMovementAnimationSpeed(AnimatorState.MovementSpeed, _movementVisualizationCoefficient + _coefficient);
            _movementVisualizationCoefficient += _movementVisualizationCoefficient;
        }
        
        public void PlayHitSound()
        {
            SoundCollection?.MeleePlayerSoundEffects.PlayHit();
        }

        public void PlayMissSound()
        {
            SoundCollection?.MeleePlayerSoundEffects.PlayMissHit();
        }

        public bool SetTrueActiveState() => IsActiveState = true;

        public bool SetFalseActiveState() => IsActiveState = false;

        public void SetSwordColliderTrue() => _sword.EnableCollider();

        public void SetSwordColliderFalse() => _sword.DisableCollider();
    }
}