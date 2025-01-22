using UnityEngine;

namespace Pools
{
    public class ParticleSystemPool : BasePool<ParticleSystem>
    {
        public ParticleSystemPool(ParticleSystem prefab, PoolSettings settings, Transform container = null) : base(prefab, settings, container)
        {
        }
        
        public ParticleSystem Get(Vector3 position, Quaternion rotation)
        {
            ParticleSystem particleSystem = base.Get();
            particleSystem.transform.position = position;
            particleSystem.transform.rotation = rotation;
            particleSystem.gameObject.SetActive(true);
            
            particleSystem.Stop();
            particleSystem.Clear();
            particleSystem.Play();

            return particleSystem;
        }

        public void Release(ParticleSystem particleSystem)
        {
            particleSystem.Stop();
            particleSystem.Clear();
            base.Release(particleSystem);
        }
    }
}