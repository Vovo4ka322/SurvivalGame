using UnityEngine;

namespace Game.Scripts.MusicComponents.EffectSounds
{
    public class MeleePlayerSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _hitClip;
        [SerializeField] private AudioSource _missHitClip;
        
        public void PlayHit() => _hitClip.Play();
        
        public void PlayMissHit() => _missHitClip.Play();
    }
}