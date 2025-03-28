using UnityEngine;

namespace Game.Scripts.MusicComponents.EffectSounds
{
    public class EnemyHybridSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _meleeAttackClip;
        [SerializeField] private AudioSource _projectileLaunchClip;
        [SerializeField] private AudioSource _projectileCollisionClip;

        public void PlayMeleeAttack() => _meleeAttackClip.Play();

        public void PlayProjectileLaunch() => _projectileLaunchClip.Play();

        public void PlayProjectileCollision() => _projectileCollisionClip.Play();
    }
}