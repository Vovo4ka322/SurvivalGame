using UnityEngine;
using Ability.ArcherAbilities.Blur;
using Game.Scripts.Interfaces;
using Weapons.RangedWeapon;

namespace Game.Scripts.PlayerComponents
{
    public class RangePlayer : Player, IVampirismable, IEvasionable, IDamagable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Bow _bow;

        private float _evasionChance;

        public float Damage { get; private set; }

        public float Coefficient { get; private set; }

        public bool IsWorking { get; private set; }

        private void OnEnable()
        {
            _bow.ArrowTouched += OnHealthRestored;
        }

        private void Start()
        {
            ChangeAttackAnimationSpeed(AnimatorState.Speed, GeneralAttackSpeed);
            Damage = GeneralDamage + _bow.BowData.Damage;
        }

        private void OnDisable()
        {
            _bow.ArrowTouched -= OnHealthRestored;
        }

        public void TakeDamage(float value)
        {
            if (!TryDodge())
                LoseHealth(value);

            if (Health.IsDead)
                Destroy(gameObject);
        }

        public void Shoot()
        {
            _bow.StartShoot(Damage);
        }

        public float SetEvasion(Blur blur) => _evasionChance = blur.Evasion;

        public bool TryDodge() => Random.value <= _evasionChance;

        public void SetCoefficient(float value) => Coefficient = value;

        public bool SetTrueVampirismState() => IsWorking = true;

        public bool SetFalseVampirismState() => IsWorking = false;

        private void OnHealthRestored()
        {
            if (IsWorking)
            {
                AddHealth(Damage * Coefficient);
                Debug.Log(Damage * Coefficient + " Damage * Coefficient");
            }

        }
    }
}