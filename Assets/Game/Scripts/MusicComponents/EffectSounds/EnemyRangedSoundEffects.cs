using UnityEngine;

namespace Game.Scripts.MusicComponents.EffectSounds
{
    public class EnemyRangedSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _projectileLaunchClip;
        [SerializeField] private AudioSource _projectileCollisionClip;

        public void PlayProjectileLaunch() => _projectileLaunchClip.Play();

        public void PlayProjectileCollision() => _projectileCollisionClip.Play();
    }
}