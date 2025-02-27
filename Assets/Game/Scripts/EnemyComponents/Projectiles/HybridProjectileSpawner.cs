using UnityEngine;

namespace Game.Scripts.EnemyComponents.Projectiles
{
    public class HybridProjectileSpawner : BaseProjectileSpawner
    {
        private BaseProjectile _currentProjectile;

        public BaseProjectile BaseProjectile => _currentProjectile;

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
            _currentProjectile.Launch(Player.transform.position, ProjectilePool);
            _currentProjectile = null;
        }

        public void SetStateProjectile(Transform transform)
        {
            _currentProjectile.transform.SetParent(transform);
        }
    }
}