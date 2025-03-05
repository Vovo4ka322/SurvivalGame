using UnityEngine;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public abstract class BaseDamageArea : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        
        protected void DealDamageToCollider(Collider enemyCollider)
        {
            if (enemyCollider.TryGetComponent(out Player player))
            {
                player.LoseHealth(_enemy.GetDamage());
            }
        }
    }
}