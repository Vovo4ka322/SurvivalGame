using UnityEngine;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.PlayerComponents;
using Weapons;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class EnemyCollider : IEnemyCollider
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
                _enemy.AnimationAnimationController.TakeHit();

                if (_enemy.Health.IsDead)
                {
                    _enemy.AnimationAnimationController.Death();
                    _player.GetExperience(_enemy.Data.Experience);
                }
            }
        }
    }
}