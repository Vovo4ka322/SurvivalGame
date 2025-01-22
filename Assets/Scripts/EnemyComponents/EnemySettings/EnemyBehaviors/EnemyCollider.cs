using UnityEngine;
using PlayerComponents;
using Weapons;

namespace EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class EnemyCollider
    {
        private readonly Enemy _enemy;
        private readonly Player _player;

        public EnemyCollider(Enemy enemy, Player player)
        {
            _enemy = enemy;
            _player = player;
        }

        public void HandleCollision(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Weapon weapon))
            {
                _enemy.Health.Lose(weapon.WeaponData.Damage);
                _enemy.AnimationController.TakeHit();

                if (_enemy.Health.IsDead)
                {
                    _enemy.AnimationController.Death();
                    _player.GetExperience(_enemy.Data.Experience);
                }
            }
        }
    }
}
