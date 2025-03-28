using System.Collections;
using UnityEngine;
using Game.Scripts.PoolComponents;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;

namespace Game.Scripts.ProjectileComponents.CollisionComponents
{
    public class ExplosionHandler : IExplosionHandler
    {
        private readonly PoolManager _poolManager;
        private readonly ParticleSystem _explosionPrefab;

        public ExplosionHandler(PoolManager poolManager, ParticleSystem explosionPrefab)
        {
            _poolManager = poolManager;
            _explosionPrefab = explosionPrefab;
        }

        public void Explode(BaseProjectile projectile)
        {
            if (_poolManager != null && _explosionPrefab != null)
            {
                ParticleSystem explosion = _poolManager.EffectsPool.Get(_explosionPrefab, projectile.transform.position, projectile.transform.rotation);

                if (explosion != null)
                {
                    _poolManager.CoroutineRunner.StartCoroutine(ReturnEffectToPool(explosion));
                }
            }
        }

        private IEnumerator ReturnEffectToPool(ParticleSystem effect)
        {
            if (effect == null)
            {
                yield break;
            }

            yield return new WaitWhile(() => effect.IsAlive(true));

            _poolManager.EffectsPool.Release(_explosionPrefab, effect);
        }
    }
}