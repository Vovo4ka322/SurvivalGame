using UnityEngine;

namespace EnemyComponents.Interfaces
{
    public interface IEnemyMovement
    {
        public bool IsMoveAllowed { get; }
        
        public void Move(Vector3 targetPosition);

        public void CanMove(bool value);
        
        public void PlayMove();
        
        public void StopMove();
    }
}