using UnityEngine;

namespace Game.Scripts.MusicComponents.EffectSounds
{
    public class RangedPlayerSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _hitClip;
        [SerializeField] private AudioSource _reloadClip;
        [SerializeField] private AudioSource _shootClip;
        
        public void PlayHit() => _hitClip.Play();
        
        public void PlayReload() => _reloadClip.Play();
        
        public void PlayShoot() => _shootClip.Play();
    }
}