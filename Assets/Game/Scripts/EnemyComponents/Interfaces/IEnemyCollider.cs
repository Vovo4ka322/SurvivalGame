using UnityEngine;

namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface IEnemyCollider
    {
        public void HandleCollision(Collider other);
    }
}