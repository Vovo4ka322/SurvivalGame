using Game.Scripts.ProjectileComponents.ProjectileInterfaces;

namespace Game.Scripts.ProjectileComponents.CreateProjectiles
{
    public class RangedProjectileSpawner : BaseProjectileSpawner
    {
        public void SpawnMagic()
        {
            if(Player == null)
            {
                return;
            }
            
            BaseProjectile projectile = Create();

            if(projectile == null)
            {
                return;
            }
            
            IExplosionHandler explosionHandler = CreateExplosionHandler();
            
            projectile.Launch(Player.transform.position, ProjectilePool, explosionHandler);
        }
    }
}