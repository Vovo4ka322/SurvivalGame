using UnityEngine;

namespace Game.Scripts.EnemyComponents.Projectiles
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
            
            _currentProjectile.Launch(Player.transform.position, ProjectilePool);
            _currentProjectile = null;
        }
        
        /*public void ResetProjectile()
        {
            if(_currentProjectile != null)
            {
                _currentProjectile.transform.SetParent(null);
                
                ProjectilePool<BaseProjectile> pool = PoolManager.GetProjectilePool(_currentProjectile);

                if(pool != null)
                {
                    pool.Release(_currentProjectile);
                }

                _currentProjectile = null;
            }
        }*/
        
        private void SetStateProjectile(Transform projectileSpawnPoint)
        {
            _currentProjectile.transform.SetParent(projectileSpawnPoint);
        }
    }
}