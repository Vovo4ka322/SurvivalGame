namespace Game.Scripts.EnemyComponents.Projectiles
{
    public class RangedProjectileSpawner : BaseProjectileSpawner
    {
        private BaseProjectile _currentProjectile;
        
        public void SpawnMagic()
        {
            if(Player == null)
            {
                return;
            }
            
            _currentProjectile = Create();

            if(_currentProjectile == null)
            {
                return;
            }
            
            _currentProjectile.Launch(Player.transform.position, ProjectilePool);
        }
    }
}