using System;
using UnityEngine;

namespace Game.Scripts.EnemyComponents.EnemySettings.Effects
{
    [Serializable]
    public struct EffectData
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Vector3 _rotationOffset;

        public EffectData(ParticleSystem effect, Vector3 positionOffset, Vector3 scale, Vector3 rotationOffset)
        {
            _effect = effect;
            _positionOffset = positionOffset;
            _scale = scale;
            _rotationOffset = rotationOffset;
        }

        public ParticleSystem EffectPrefab => _effect;
        public Vector3 PositionOffset => _positionOffset;
        public Vector3 RotationOffset => _rotationOffset;
        public Vector3 Scale => _scale;
    }
}