using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Scripts.MusicComponents
{
    public class AudioGameSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioParameterNames _audioParams;

        private readonly float _defaultVolume = 0.75f;
        private readonly float _fadeDuration = 2f;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }

        public void SetVolume(string parameterName, float volume, bool save = true)
        {
            float clampedVolume = Mathf.Clamp(volume, 0.0001f, 1f);
            float dbVolume = Mathf.Log10(clampedVolume) * 20f;

            _audioMixer.SetFloat(parameterName, dbVolume);

            if (save)
            {
                PlayerPrefs.SetFloat(parameterName, volume);
                PlayerPrefs.Save();
            }
        }

        private void LoadSettings()
        {
            float allVol = PlayerPrefs.GetFloat(_audioParams.AllSoundVolume, _defaultVolume);
            float musicVol = PlayerPrefs.GetFloat(_audioParams.MusicVolume, _defaultVolume);
            float effectsVol = PlayerPrefs.GetFloat(_audioParams.EffectsVolume, _defaultVolume);

            SetVolume(_audioParams.AllSoundVolume, 0f, false);
            SetVolume(_audioParams.MusicVolume, 0f, false);
            SetVolume(_audioParams.EffectsVolume, 0f, false);

            StartCoroutine(FadeInVolume(_audioParams.AllSoundVolume, allVol, _fadeDuration));
            StartCoroutine(FadeInVolume(_audioParams.MusicVolume, musicVol, _fadeDuration));
            StartCoroutine(FadeInVolume(_audioParams.EffectsVolume, effectsVol, _fadeDuration));
        }

        private IEnumerator FadeInVolume(string parameterName, float targetVolume, float duration)
        {
            float time = 0f;

            while (time < duration)
            {
                float newVolume = Mathf.Lerp(0f, targetVolume, time / duration);

                SetVolume(parameterName, newVolume, false);

                time += Time.deltaTime;

                yield return null;
            }

            SetVolume(parameterName, targetVolume, false);
        }
    }
}