using UnityEngine;
using Game.Scripts.EnemyComponents;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;

namespace Game.Scripts.ProjectileComponents.CreateProjectiles
{
    public class HybridProjectileSpawner : BaseProjectileSpawner
    {
        private BaseProjectile _currentProjectile;

        public void PrepareStone()
        {
            _currentProjectile = Create();
            
            if(_currentProjectile == null)
            {
                return;
            }
            
            SetStateProjectile(ProjectileSpawnPoint);
        }
    
        public void ThrowStone()
        {
            if(_currentProjectile == null || Player == null)
            {
                return;
            }
            
            SetStateProjectile(null);
            
            if(TryGetComponent(out Enemy enemy))
            {
                _currentProjectile.SetOwner(enemy);
            }
            
            IExplosionHandler explosionHandler = CreateExplosionHandler();
            
            _currentProjectile.Launch(Player.transform.position, ProjectilePool, explosionHandler);
            _currentProjectile = null;
        }
        
        private void SetStateProjectile(Transform projectileSpawnPoint)
        {
            _currentProjectile.transform.SetParent(projectileSpawnPoint);
        }
    }
}