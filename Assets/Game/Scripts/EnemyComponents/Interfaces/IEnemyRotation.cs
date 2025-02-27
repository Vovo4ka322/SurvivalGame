using UnityEngine;

namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface IEnemyRotation
    {
        public void RotateTowards(Vector3 targetPosition);
    }
}