using UnityEngine;

namespace EnemyComponents.Interfaces
{
    public interface IEnemyMovement
    {
        public void Move(Vector3 targetPosition);
        public void PlayMove();
        public void StopMove();
    }
}