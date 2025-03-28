using System.Collections;
using UnityEngine;
using Game.Scripts.PlayerComponents;
using Game.Scripts.PoolComponents;

namespace Game.Scripts.Weapons.MeleeWeapon
{
    public class Sword : Weapon
    {
        [SerializeField] private Collider _swordCollider;
        [SerializeField] private ParticleSystem _attackEffectPrefab;

        private PoolManager _poolManager;
        private MeleePlayer _player;
        private bool _hitRegistered = false;
        private bool _useNegativeRotation = true;

        private void Awake()
        {
            _swordCollider.isTrigger = true;
        }

        public void SetPoolManager(PoolManager poolManager)
        {
            _poolManager = poolManager;
        }

        public void SetPlayer(MeleePlayer player)
        {
            _player = player;
        }

        public void EnableCollider()
        {
            _hitRegistered = false;
            _swordCollider.enabled = true;
            PlayAttackEffect();
        }

        public void DisableCollider()
        {
            _swordCollider.enabled = false;

            if (!_hitRegistered)
            {
                _player?.PlayMissSound();
            }
        }

        public void RegisterHit()
        {
            _hitRegistered = true;
            _player?.PlayHitSound();
        }

        private void PlayAttackEffect()
        {
            if (_poolManager == null || _attackEffectPrefab == null)
            {
                return;
            }

            Vector3 spawnPosition = _player.transform.position + Vector3.up * 1f;
            float xRotation = _useNegativeRotation ? -90f : 90f;
            _useNegativeRotation = !_useNegativeRotation;
            Quaternion effectRotation = Quaternion.Euler(xRotation, _attackEffectPrefab.transform.eulerAngles.y, _attackEffectPrefab.transform.eulerAngles.z);

            ParticleSystem effectInstance = _poolManager.EffectsPool.Get(_attackEffectPrefab, spawnPosition, effectRotation);

            if (effectInstance != null)
            {
                effectInstance.transform.SetParent(_player.transform, true);
                effectInstance.transform.rotation = effectRotation;
                StartCoroutine(ReturnEffectToPool(effectInstance, _attackEffectPrefab));
            }
        }

        private IEnumerator ReturnEffectToPool(ParticleSystem effectInstance, ParticleSystem prefab)
        {
            float waitTime = effectInstance.main.duration;

            yield return new WaitForSeconds(waitTime);

            _poolManager.EffectsPool.Release(prefab, effectInstance);
        }
    }
}