using UnityEngine;
using EnemyComponents.Interfaces;

namespace EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class PlayerNavigator
    {
        private readonly IEnemyMovement _movement;
        private readonly Transform _playerTransform;

        public PlayerNavigator(IEnemyMovement movement, Transform playerTransform)
        {
            _movement = movement;
            _playerTransform = playerTransform;
        }

        public void MoveTowardsPlayer()
        {
            _movement.PlayMove();
            _movement.Move(_playerTransform.position);
        }
    }
}