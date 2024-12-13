using Enemies;
using UnityEngine;

namespace MainPlayer
{
    public class Player : Character, IHealable
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private BuffHolder _buffHolder;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerLevel _level = new();

        [SerializeField] private Buff _buff;//временно. Потом перенести в магазин

        public float AttackSpeed { get; private set; }

        public int Power { get; private set; }

        public int Armor { get; private set; }

        public bool IsHealState { get; private set; }

        public PlayerLevel Level => _level;

        private void Awake()
        {
            _level.Init();
            _buffHolder.Add(_buff);// покупка бафов через магазин
            Init(_characterType);
        }

        public void Init(ICharacteristicable characteristicable)
        {
            float health = characteristicable.MaxHealth;
            Power = characteristicable.Power;
            Armor = characteristicable.Armor;

            foreach (var buff in _buffHolder.Baffs)
            {
                Power += buff.Power;
                Armor += buff.Armor;
                health += buff.MaxHealth;
            }

            Health.InitMaxValue(health);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (IsHealState)
                {
                    Health.Add(enemy.SetDamage());//потом сделать условие для бойца дальнего боя
                }
                else
                {
                    Health.Lose(enemy.SetDamage());
                }

                if (Health.IsDead)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void GetExperience(int value)
        {
            _level.GainExperience(value);
            Debug.Log(_level.Experience + " текущий опыт");
            Debug.Log(_level.Level + " текущий lvl");
        }

        public void UpgradeCharacteristikByBloodlust(Bloodlust bloodlust)
        {
            _playerMovement.ChangeMoveSpeed(bloodlust.MovementSpeed);
            AttackSpeed += bloodlust.AttackSpeed;
        }

        public void SetState(bool state)
        {
            IsHealState = state;
        }
    }
}