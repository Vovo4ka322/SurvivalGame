using UnityEngine;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class HybridMeleeDamageArea : BaseDamageArea
    {
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _damageRadius = 3f;
        
        public void DealAreaDamage()
        {
            Collider[] hits = Physics.OverlapSphere(_attackPoint.position, _damageRadius, _targetLayer);
            
            foreach (Collider hit in hits)
            {
                DealDamageToCollider(hit);
            }
        }
        
        /*private void OnDrawGizmosSelected()
        {
            if (_attackPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_attackPoint.position, _damageRadius);
            }
        }*/
    }
}