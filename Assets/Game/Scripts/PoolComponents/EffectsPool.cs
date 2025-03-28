using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PoolComponents
{
    public class EffectsPool
    {
        private readonly Dictionary<ParticleSystem, BasePool<ParticleSystem>> _effectPools = new Dictionary<ParticleSystem, BasePool<ParticleSystem>>();

        private PoolSettings _poolSettings;
        private Transform _container;

        public void Initialize(List<ParticleSystem> effectPrefabs, PoolSettings poolSettings, Transform container)
        {
            _poolSettings = poolSettings;
            _container = container;

            foreach (var prefab in effectPrefabs)
            {
                if (prefab != null && !_effectPools.ContainsKey(prefab))
                {
                    var pool = new ParticleSystemPool<ParticleSystem>(prefab, _poolSettings, _container);
                    _effectPools.Add(prefab, pool);
                }
            }
        }

        public ParticleSystem Get(ParticleSystem prefab, Vector3 position, Quaternion rotation)
        {
            if (prefab == null || !_effectPools.TryGetValue(prefab, out var pool))
            {
                return null;
            }

            ParticleSystem instance = pool.Get();

            if (instance != null)
            {
                instance.transform.position = position;
                instance.transform.rotation = rotation;
                instance.transform.localScale = Vector3.one;
                instance.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                instance.Play();
            }

            return instance;
        }

        public void Release(ParticleSystem prefab, ParticleSystem effect)
        {
            if (prefab == null || !_effectPools.ContainsKey(prefab))
            {
                return;
            }

            var pool = _effectPools[prefab];
            pool.Release(effect);
        }
    }
}