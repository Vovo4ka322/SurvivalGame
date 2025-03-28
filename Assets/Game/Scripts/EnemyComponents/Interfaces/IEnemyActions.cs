using Game.Scripts.EnemyComponents.EnemySettings;
using Game.Scripts.PlayerComponents;
using UnityEngine;

namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface IEnemyActions
    {
        public IEnemyMovement Movement { get; }
        public IEnemyAttack EnemyAttack { get; }
        public EnemyData Data { get; }
        public Player Player { get; }

        public void SetTargetPosition(Vector3 target);

        public float GetDamage();
    }
}
