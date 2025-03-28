using UnityEngine;

namespace Game.Scripts.MusicComponents.EffectSounds
{
    public class BossSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _hitClip;
        [SerializeField] private AudioSource _missHitClip;
        [SerializeField] private AudioSource _meleeAttack1Clip;
        [SerializeField] private AudioSource _meleeAttack2Clip;

        public void PlayMeleeHit() => _hitClip.Play();

        public void PlayMeleeMissHit() => _missHitClip.Play();

        public void PlayMeleeAttack1() => _meleeAttack1Clip.Play();

        public void PlayMeleeAttack2() => _meleeAttack2Clip.Play();
    }
}
