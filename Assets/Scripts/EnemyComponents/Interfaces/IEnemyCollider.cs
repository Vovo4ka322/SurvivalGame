using UnityEngine;

namespace EnemyComponents.Interfaces
{
    public interface IEnemyCollider
    {
        public void HandleCollision(Collider other);
    }
}