using Enemies;
using UnityEngine;

namespace MainPlayer
{
    public class Player : Character, IActivable, IVampirismable, IEvasionable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private BuffHolder _buffHolder;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerLevel _level = new();
        [SerializeField] private Bow _bow;
        [SerializeField] private Sword _sword;

        [SerializeField] private Buff _buff;//временно. Потом перенести в магазин

        private float _evasionChance;

        public float WeaponDamage { get; private set; }

        public float AttackSpeed { get; private set; }

        public int Damage { get; private set; }

        public int Armor { get; private set; }

        public bool IsActiveState { get; private set; }

        public bool IsWorking { get; private set; }

        public float Coefficient { get; private set; }

        public PlayerLevel Level => _level;

        private void Awake()
        {
            _level.Init();
            _buffHolder.Add(_buff);// покупка бафов через магазин
            Init(_characterType);

            WeaponDamage = _weapon.WeaponData.Damage;
        }

        private void OnEnable()
        {
            _bow.ArrowTouched += OnHealthRestored;
        }

        private void OnDisable()
        {
            _bow.ArrowTouched -= OnHealthRestored;
        }

        private void OnHealthRestored()
        {
            if (IsWorking)
            {
                Health.Add(WeaponDamage * Coefficient);
            }
        }

        public void Init(ICharacteristicable characteristicable)
        {
            float health = characteristicable.MaxHealth;
            Damage = characteristicable.Damage;
            Armor = characteristicable.Armor;

            foreach (var buff in _buffHolder.Baffs)
            {
                Damage += buff.Damage;
                Armor += buff.Armor;
                health += buff.MaxHealth;
            }

            Health.InitMaxValue(health);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (IsActiveState)
                {
                    Health.Add(enemy.SetDamage());
                }
                else
                {
                    if (TryDodge())
                    {
                        Physics.IgnoreCollision(_collider, enemy.Collider);
                    }
                    else
                    {
                        Health.Lose(enemy.SetDamage());
                    }
                }

                if (Health.IsDead)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void SetWeaponeDamage()
        {
            WeaponDamage = _weapon.WeaponData.Damage;
        }

        public void GetExperience(int value)
        {
            _level.GainExperience(value);
            //Debug.Log(_level.Experience + " текущий опыт");
            //Debug.Log(_level.Level + " текущий lvl");
        }

        public void UpgradeCharacteristikByBloodlust(Bloodlust bloodlust)
        {
            _playerMovement.ChangeMoveSpeed(bloodlust.MovementSpeed);
            AttackSpeed += bloodlust.AttackSpeed;
        }

        public float SetEvasion(Blur blur) => _evasionChance = blur.Evasion;

        public bool SetTrueActiveState() => IsActiveState = true;

        public bool SetFalseActiveState() => IsActiveState = false;

        public bool SetTrueVampirismState() => IsWorking = true;

        public bool SetFalseVampirismState() => IsWorking = false;

        public void SetCoefficient(float value) => Coefficient = value;

        public bool TryDodge() => Random.value <= _evasionChance;
    }
}