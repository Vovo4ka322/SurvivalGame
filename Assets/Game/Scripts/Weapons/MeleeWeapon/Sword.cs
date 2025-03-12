using System;
using UnityEngine;
using Game.Scripts.PlayerComponents;

namespace Weapons.MeleeWeapon
{
    public class Sword : Weapon
    {
        [SerializeField] private MeshCollider _meshCollider;
        
        private MeleePlayer _player;
        private bool _hitRegistered = false;

        private void Awake()
        {
            _meshCollider.isTrigger = true;
        }

        public void SetPlayer(MeleePlayer player)
        {
            _player = player;
        }
        
        public void EnableCollider()
        {
            _hitRegistered = false;
            _meshCollider.enabled = true;
        }
        
        public void DisableCollider()
        {
            _meshCollider.enabled = false;
            
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
    }
}