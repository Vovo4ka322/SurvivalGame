using UnityEngine;
using UnityEngine.Audio;

namespace Music
{
    public class GameAudioPlayback : MonoBehaviour
    {
        [SerializeField] private AudioSource _backgroundMusic;
        [SerializeField] private AudioMixer _audioMixer;
        
        public void PlayBackgroundMusic()
        {
            if(_backgroundMusic != null)
            {
                _backgroundMusic.loop = true;
                _backgroundMusic.Play();
            }
        }

        public void SetVolume(string parameterName, float volume)
        {
            float clampedVolume = Mathf.Clamp(volume, 0.0001f, 1f);
            float dbVolume = Mathf.Log10(clampedVolume) * 20;

            _audioMixer.SetFloat(parameterName, dbVolume);
            PlayerPrefs.SetFloat(parameterName, volume);
            PlayerPrefs.Save(); 
        }
    }
}