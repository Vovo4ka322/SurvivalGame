using UnityEngine;
using Game.Scripts.PlayerComponents;
using Game.Scripts.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public abstract class BaseDamageArea : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        
        protected void DealDamageToCollider(Collider enemyCollider)
        {
            if (enemyCollider.TryGetComponent(out IDamagable player))
            {
                player.TakeDamage(_enemy.GetDamage());

            }
        }
    }
}