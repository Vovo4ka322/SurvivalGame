using UnityEngine;

namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface IEnemyMovement
    {
        public void ProcessMovement(Vector3 targetPosition, bool spawnCompleted, bool isAttacking);

        public void CanMove(bool value);

        public void StartMoving();

        public void Stop();
    }
}