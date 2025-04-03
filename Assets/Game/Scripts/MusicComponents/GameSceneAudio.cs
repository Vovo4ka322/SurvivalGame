using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Scripts.MusicComponents
{
    public class GameSceneAudio : MonoBehaviour
    {
        private readonly float _mutedVolume = -80f;

        [SerializeField] private AudioParameterNames _audioParams;
        [SerializeField] private AudioSource _waveMusicSource;
        [SerializeField] private AudioSource _bossMusicSource;
        [SerializeField] private AudioMixer _audioMixer;

        private float _originalVolume;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (!_audioMixer.GetFloat(_audioParams.AllSoundVolume, out _originalVolume))
            {
                _originalVolume = 0f;
            }

            if (_waveMusicSource != null)
            {
                _waveMusicSource.loop = true;
                _waveMusicSource.volume = 1f;
                _waveMusicSource.Play();
            }

            if (_bossMusicSource != null)
            {
                _bossMusicSource.loop = true;
                _bossMusicSource.volume = 0f;
                _bossMusicSource.Play();
            }
        }

        public void StopAllMusic()
        {
            _waveMusicSource?.Stop();
            _bossMusicSource?.Stop();
        }

        public void SwitchToBossMusic(float fadeDuration = 2f)
        {
            StartCoroutine(CrossfadeMusic(_waveMusicSource, _bossMusicSource, fadeDuration));
        }

        private void OnApplicationPause(bool hasFocus)
        {
            if (hasFocus)
            {
                _waveMusicSource?.UnPause();
                _bossMusicSource?.UnPause();
                _audioMixer.SetFloat(_audioParams.AllSoundVolume, _originalVolume);
            }
            else
            {
                _waveMusicSource?.Pause();
                _bossMusicSource?.Pause();
                _audioMixer.SetFloat(_audioParams.AllSoundVolume, _mutedVolume);
            }
        }

        private IEnumerator CrossfadeMusic(AudioSource fromSource, AudioSource toSource, float duration)
        {
            float time = 0f;
            float fromInitialVolume = fromSource.volume;
            float toInitialVolume = toSource.volume;

            if (!toSource.isPlaying)
            {
                toSource.Play();
            }

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;
                fromSource.volume = Mathf.Lerp(fromInitialVolume, 0f, t);
                toSource.volume = Mathf.Lerp(toInitialVolume, 1f, t);

                yield return null;
            }

            fromSource.volume = 0f;
            toSource.volume = 1f;
        }
    }
}