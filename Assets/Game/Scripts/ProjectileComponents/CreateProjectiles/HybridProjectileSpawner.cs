using Game.Scripts.EnemyComponents;
using Game.Scripts.MusicComponents.EffectSounds;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;
using UnityEngine;

namespace Game.Scripts.ProjectileComponents.CreateProjectiles
{
    public class HybridProjectileSpawner : BaseProjectileSpawner
    {
        private BaseProjectile _currentProjectile;
        private SoundCollection _soundCollection;

        public void SetSound(SoundCollection soundCollection)
        {
            _soundCollection = soundCollection;
        }

        public void CancelPreparedProjectile()
        {
            if (_currentProjectile != null)
            {
                _currentProjectile.ExplodeAndReturn();
                _currentProjectile = null;
            }
        }

        public void PrepareStone()
        {
            if (_currentProjectile != null && _currentProjectile.gameObject.activeSelf)
            {
                return;
            }

            _currentProjectile = Create();

            if (_currentProjectile == null)
            {
                return;
            }

            _currentProjectile.SetColliderActive(false);
            SetStateProjectile(ProjectileSpawnPoint);
        }

        public void ThrowStone()
        {
            if (_currentProjectile == null || Player == null)
            {
                return;
            }

            _currentProjectile.SetColliderActive(true);
            SetStateProjectile(null);

            if (TryGetComponent(out Enemy enemy))
            {
                _currentProjectile.SetOwner(enemy);
            }

            IExplosionHandler explosionHandler = CreateExplosionHandler();

            _currentProjectile.Launch(Player.transform.position, ProjectilePool, explosionHandler);

            if (_soundCollection != null && _soundCollection.HybridSoundEffects != null)
            {
                _soundCollection.HybridSoundEffects.PlayProjectileLaunch();
            }

            _currentProjectile = null;
        }

        private void SetStateProjectile(Transform projectileSpawnPoint)
        {
            _currentProjectile.transform.SetParent(projectileSpawnPoint);
        }
    }
}