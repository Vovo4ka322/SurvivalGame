namespace EnemyComponents.Projectiles
{
    public class RangedProjectileSpawner : BaseProjectileSpawner
    {
        public void SpawnMagic()
        {
            if(Player == null)
            {
                return;
            }
            
            BaseProjectile projectile = SpawnProjectile();

            if(projectile == null)
            {
                return;
            }
            
            projectile.Launch(Player.transform.position, ProjectilePool);
        }
    }
}