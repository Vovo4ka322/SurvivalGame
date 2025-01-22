using System;
using UnityEngine;

namespace EnemyComponents.EnemySettings.Effects
{
    [Serializable]
    public class EffectData
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _scale = Vector3.one;
        [SerializeField] private Vector3 _rotationOffset;

        public ParticleSystem EffectPrefab => _effect;
        public Vector3 PositionOffset => _positionOffset;
        public Vector3 RotationOffset => _rotationOffset;
        public Vector3 Scale => _scale;
    }
}