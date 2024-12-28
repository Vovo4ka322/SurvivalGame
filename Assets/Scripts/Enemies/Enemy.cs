using PlayerComponents;
using UnityEngine;

namespace Enemies
{
    public class Enemy : Character
    {
        [SerializeField] private EnemyMovement _movement;

        private EnemyData _data;
        private Transform _target;
        private Player _player;

        [field:SerializeField] public Collider Collider {  get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Weapon weapon))
            {
                Health.Lose(weapon.WeaponData.Damage);

                if (Health.IsDead)
                {
                    Destroy(gameObject);
                    _player.GetExperience(_data.Experience);
                }
            }
        }

        private void Update()
        {
            if (_target != null)
                _movement.Move(_target, _data.MoveSpeed);
        }

        public void Init(EnemyData data, Player player)
        {
            _target = player.transform;
            _data = data;
            _player = player;
            Health.InitMaxValue(data.MaxHealth);
        }

        public float SetDamage()
        {
            return _data.Damage;
        }
    }
}