using UnityEngine;

namespace Game.Scripts.MusicComponents.EffectSounds
{
    public class EnemyMeleeSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _meleeHitClip;
        [SerializeField] private AudioSource _meleeMissClip;

        public void PlayHit() => _meleeHitClip.Play();
        
        public void PlayMissHit() => _meleeMissClip.Play();
    }
}