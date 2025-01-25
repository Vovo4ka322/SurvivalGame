using UnityEngine;

namespace EnemyComponents.Interfaces
{
    public interface IEnemyRotation
    {
        public void RotateTowards(Vector3 targetPosition);
    }
}