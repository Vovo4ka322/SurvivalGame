using System;
using UnityEngine;
using Game.Scripts.Interfaces;
using Game.Scripts.PoolComponents;
using Game.Scripts.ProjectileComponents;

namespace Weapons.RangedWeapon
{
    public class Bow : Weapon, IActivable
    {
        [SerializeField] private ArrowSpawner _arrowSpawner;
        [SerializeField] private BowData _bowData;

        private ArrowProjectile _arrowProjectile;
        private Coroutine _arrowCreatorCoroutine;

        public event Action ArrowTouched;

        [field: SerializeField] public Transform StartPointToFly {  get; private set; }
        public PoolManager PoolManager { get; set; }
        public BowData BowData => _bowData;
        public bool IsActiveState { get; private set; }

        public void StartShoot()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (!IsActiveState)
            {
                if(_arrowProjectile != null)
                {
                    _arrowProjectile.Touched -= OnTouched;
                }

                ArrowProjectile arrowProjectile = _arrowSpawner.Spawn();
                
                if (arrowProjectile != null)
                {
                    Vector3 targetPos = StartPointToFly.position + StartPointToFly.forward * _bowData.AttackRadius;
                    ProjectilePool<BaseProjectile> pool = PoolManager.GetProjectilePool(arrowProjectile);
                    
                    arrowProjectile.Launch(targetPos, pool, null);
                    
                    _arrowProjectile = arrowProjectile;
                    
                    _arrowProjectile.Touched += OnTouched;
                }
            }
        }

        public void OnTouched()
        {
            ArrowTouched?.Invoke();
        }

        public bool SetTrueActiveState() => IsActiveState = true;

        public bool SetFalseActiveState() => IsActiveState = false;
    }
}