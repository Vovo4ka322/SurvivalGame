namespace EnemyComponents.Projectiles
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
            
            _currentProjectile.transform.SetParent(ProjectileSpawnPoint);
        }
    
        public void ThrowStone()
        {
            if(_currentProjectile == null || Player == null)
            {
                return;
            }
            
            _currentProjectile.transform.SetParent(null);
            _currentProjectile.Launch(Player.transform.position, ProjectilePool);
            _currentProjectile = null;
        }
    }
}