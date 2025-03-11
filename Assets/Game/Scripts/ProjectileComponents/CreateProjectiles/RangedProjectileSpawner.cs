using Game.Scripts.EnemyComponents;
using Game.Scripts.MusicComponents.EffectSounds;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;

namespace Game.Scripts.ProjectileComponents.CreateProjectiles
{
    public class RangedProjectileSpawner : BaseProjectileSpawner
    {
        private SoundCollection _soundCollection;

        public void SetSoundCollection(SoundCollection collection)
        {
            _soundCollection = collection;
        }
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
            
            if (TryGetComponent(out Enemy enemy))
            {
                projectile.SetOwner(enemy);
            }
            
            IExplosionHandler explosionHandler = CreateExplosionHandler();
            
            projectile.Launch(Player.transform.position, ProjectilePool, explosionHandler);

            if(_soundCollection != null && _soundCollection.RangedSoundEffects != null)
            {
                _soundCollection.RangedSoundEffects.PlayProjectileLaunch();
            }
        }
    }
}