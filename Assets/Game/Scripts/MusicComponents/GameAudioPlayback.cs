using UnityEngine;
using UnityEngine.Audio;

namespace Game.Scripts.MusicComponents
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
        
        public void StopBackgroundMusic()
        {
            _backgroundMusic?.Stop();
        }
        
        private void OnApplicationPause(bool hasFocus)
        {
            if (_backgroundMusic != null)
            {
                if (hasFocus)
                {
                    _backgroundMusic.UnPause();
                }
                else
                {
                    _backgroundMusic.Pause();
                }
            }
        }
    }
}