using System.Collections;
using UnityEngine;
using Pools;

namespace EnemyComponents.EnemySettings.Effects
{
    public class BaseEffectsEnemy
    {
        private readonly MonoBehaviour _owner;
        private readonly EffectData _effectData;
        private readonly ParticleSystemPool _pool;
        private readonly PoolSettings _poolSettings;
        
        private ParticleSystem _currentEffect;
        private Coroutine _currentCoroutine;
        
        public BaseEffectsEnemy(MonoBehaviour owner, EffectData effectData, PoolSettings poolSettings)
        {
            _owner = owner;
            _effectData = effectData;
            _poolSettings = poolSettings;
            _pool = TryCreatePool(effectData);
        }
        
        public void Play()
        {
            _currentEffect = Create();
            
            if(_currentEffect != null)
            {
                _currentCoroutine = _owner.StartCoroutine(WaitAndReturn(_currentEffect));
            }
        }

        public void Stop()
        {
            if(_currentCoroutine != null)
            {
                _owner.StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
            
            if(_currentEffect != null)
            {
                StopAndReturn(_currentEffect);
                _currentEffect = null;
            }
        }

        private ParticleSystem Create()
        {
            if(_effectData == null || _pool == null) return null;
        
            Vector3 position = _owner.transform.position + _owner.transform.TransformDirection(_effectData.PositionOffset);
            Quaternion rotation = _owner.transform.rotation * Quaternion.Euler(_effectData.RotationOffset);
            
            ParticleSystem effectInstance = _pool.Get(position, rotation);
            effectInstance.transform.localScale = _effectData.Scale;
            
            return effectInstance;
        }
        
        private ParticleSystemPool TryCreatePool(EffectData data)
        {
            if(data != null && data.EffectPrefab != null)
            {
                return new ParticleSystemPool(data.EffectPrefab, _poolSettings, _owner.transform);
            }
            
            return null;
        }

        private IEnumerator WaitAndReturn(ParticleSystem effect)
        {
            yield return new WaitWhile(() => effect.IsAlive(true));
            
            StopAndReturn(effect);
        }

        private void StopAndReturn(ParticleSystem effect)
        {
            if(effect == null || _pool == null)
            {
                return;
            }
            
            _pool.Release(effect);
        }
    }
}