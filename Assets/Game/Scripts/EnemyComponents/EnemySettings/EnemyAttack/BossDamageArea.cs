using UnityEngine;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack
{
    public class BossDamageArea : BaseDamageArea
    {
        [Header("Radius attacks")]
        [SerializeField] private Transform _areaAttackPoint1;
        [SerializeField] private Transform _areaAttackPoint2;
        [SerializeField] private LayerMask _areaTargetLayer;
        [SerializeField] private float _areaDamageRadius1 = 3f;
        [SerializeField] private float _areaDamageRadius2 = 3f;

        [Header("Middle battle attack")]
        [SerializeField] private Collider _meleeDamageCollider;

        [Header("Direct attack")]
        [SerializeField] private Transform _directAttackPoint;
        [SerializeField] private Vector3 _directAttackBoxSize = new Vector3(3f, 2f, 5f);
        [SerializeField] private LayerMask _directTargetLayer;

        public void DealRadialDamage1()
        {
            Collider[] hits = Physics.OverlapSphere(_areaAttackPoint1.position, _areaDamageRadius1, _areaTargetLayer);

            foreach(Collider hit in hits)
            {
                DealDamageToCollider(hit);
            }
        }

        public void DealRadialDamage2()
        {
            Collider[] hits = Physics.OverlapSphere(_areaAttackPoint2.position, _areaDamageRadius2, _areaTargetLayer);

            foreach(Collider hit in hits)
            {
                DealDamageToCollider(hit);
            }
        }

        public void EnableMeleeDamageCollider()
        {
            _meleeDamageCollider.enabled = true;
        }

        public void DisableMeleeDamageCollider()
        {
            _meleeDamageCollider.enabled = false;
        }

        public void DealMeleeDamageIfEnabled(Collider other)
        {
            if(_meleeDamageCollider == null || !_meleeDamageCollider.enabled)
            {
                return;
            }

            DealDamageToCollider(other);
        }

        public void DealDirectDamage()
        {
            Collider[] hits = Physics.OverlapBox(_directAttackPoint.position, _directAttackBoxSize * 0.5f, _directAttackPoint.rotation, _directTargetLayer);

            foreach(Collider hit in hits)
            {
                DealDamageToCollider(hit);
            }
        }

        /*private void OnDrawGizmosSelected()
        {
            if(_directAttackPoint != null)
            {
                Gizmos.color = Color.green;
                Gizmos.matrix = Matrix4x4.TRS(_directAttackPoint.position, _directAttackPoint.rotation, Vector3.one);
                Gizmos.DrawWireCube(Vector3.zero, _directAttackBoxSize);
                Gizmos.matrix = Matrix4x4.identity;
            }

            if(_areaAttackPoint1 != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(_areaAttackPoint1.position, _areaDamageRadius1);
            }

            if(_areaAttackPoint2 != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(_areaAttackPoint2.position, _areaDamageRadius2);
            }
        }*/
    }
}
