using System;
using UnityEngine;
using Game.Scripts.PoolComponents;
using Game.Scripts.ProjectileComponents;

namespace Weapons.RangedWeapon
{
    public class ArrowSpawner : MonoBehaviour
    {
        [SerializeField] private ArrowProjectile _arrowPrefab;

        private ProjectilePool<BaseProjectile> _pool;

        public PoolManager PoolManager { get; set; }
        
        public ArrowProjectile Spawn()
        {
            if (PoolManager == null)
            {
                throw new ArgumentException("PoolManager is not specified in Arrowspawner.");
            }
            
            ProjectilePool<BaseProjectile> pool = PoolManager.GetProjectilePool(_arrowPrefab);
            ArrowProjectile arrow = pool.Get() as ArrowProjectile;
            _pool = pool;
            
            if (arrow != null)
            {
                arrow.transform.position = transform.position;
                arrow.transform.rotation = transform.rotation;
                arrow.gameObject.SetActive(true);
            }
            
            return arrow;
        }

        public void ReturnInPool(ArrowProjectile arrowProjectile)
        {
            _pool.Release(arrowProjectile);
        }
    }
}