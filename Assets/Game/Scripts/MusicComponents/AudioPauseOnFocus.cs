using UnityEngine;
using UnityEngine.Audio;

namespace Game.Scripts.MusicComponents
{
    public class AudioPauseOnFocus : MonoBehaviour
    {
        private readonly float _mutedVolume = -80f;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioParameterNames _audioParameterNames;

        private float _originalVolume = 0f;

        private void Start()
        {
            if (!_audioMixer.GetFloat(_audioParameterNames.AllSoundVolume, out _originalVolume))
            {
                _originalVolume = 0f;
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                _audioMixer.SetFloat(_audioParameterNames.AllSoundVolume, _originalVolume);
            }
            else
            {
                _audioMixer.SetFloat(_audioParameterNames.AllSoundVolume, _mutedVolume);
            }
        }
    }
}