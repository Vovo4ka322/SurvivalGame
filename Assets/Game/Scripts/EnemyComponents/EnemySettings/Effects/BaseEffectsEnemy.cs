using System.Collections;
using UnityEngine;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.PoolComponents;

namespace Game.Scripts.EnemyComponents.EnemySettings.Effects
{
    public class BaseEffectsEnemy
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly EffectData _effectData;
        private readonly EffectsPool _pool;

        private ParticleSystem _currentEffect;
        private Coroutine _currentCoroutine;

        protected BaseEffectsEnemy(ICoroutineRunner coroutineRunner, EffectData effectData, EffectsPool pool)
        {
            _coroutineRunner = coroutineRunner;
            _effectData = effectData;
            _pool = pool;
        }

        public void Play(Transform ownerTransform)
        {
            if (_effectData.EffectPrefab == null || _pool == null)
            {
                return;
            }

            Vector3 position = ownerTransform.transform.position + ownerTransform.transform.TransformDirection(_effectData.PositionOffset);
            Quaternion rotation = ownerTransform.transform.rotation * Quaternion.Euler(_effectData.RotationOffset);
            ParticleSystem effectInstance = _pool.Get(_effectData.EffectPrefab, position, rotation);

            if (effectInstance != null)
            {
                effectInstance.transform.localScale = _effectData.Scale;
                _currentEffect = effectInstance;
                _currentCoroutine = _coroutineRunner.StartCoroutine(WaitAndReturn(effectInstance));
            }
        }

        public void Stop()
        {
            if (_currentCoroutine != null)
            {
                _coroutineRunner.StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }

            if (_currentEffect != null)
            {
                StopAndReturn();
                _currentEffect = null;
            }
        }

        private IEnumerator WaitAndReturn(ParticleSystem effectInstance)
        {
            yield return new WaitWhile(() => effectInstance.IsAlive(true));
            StopAndReturn();
        }

        private void StopAndReturn()
        {
            if (_currentEffect == null || _effectData.EffectPrefab == null || _pool == null)
            {
                return;
            }

            _pool.Release(_effectData.EffectPrefab, _currentEffect);
        }
    }
}