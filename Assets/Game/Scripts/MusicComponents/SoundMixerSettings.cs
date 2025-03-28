using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MusicComponents
{
    public class SoundMixerSettings : MonoBehaviour
    {
        [SerializeField] private AudioParameterNames _audioParams;
        [SerializeField] private AudioGameSettings _audioGameSettings;
        [SerializeField] private Slider _generalSoundSlider;
        [SerializeField] private Slider _musicSoundSlider;
        [SerializeField] private Slider _effectSoundSlider;
        [SerializeField] private Toggle _muteToggle;

        private float _prevGeneralVolume;
        private float _prevMusicVolume;
        private float _prevEffectVolume;

        private readonly float _minValue = 0.0001f;
        private readonly float _defaultVolume = 0.75f;

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            _muteToggle?.onValueChanged.RemoveListener(OnMuteToggleChanged);
        }

        private void Initialize()
        {
            InitializeSlider(_audioParams.AllSoundVolume, _generalSoundSlider, _defaultVolume);
            InitializeSlider(_audioParams.MusicVolume, _musicSoundSlider, _defaultVolume);
            InitializeSlider(_audioParams.EffectsVolume, _effectSoundSlider, _defaultVolume);

            _generalSoundSlider.onValueChanged.AddListener(volume => _audioGameSettings.SetVolume(_audioParams.AllSoundVolume, volume));
            _musicSoundSlider.onValueChanged.AddListener(volume => _audioGameSettings.SetVolume(_audioParams.MusicVolume, volume));
            _effectSoundSlider.onValueChanged.AddListener(volume => _audioGameSettings.SetVolume(_audioParams.EffectsVolume, volume));

            _muteToggle.onValueChanged.AddListener(OnMuteToggleChanged);
        }

        private void InitializeSlider(string parameterName, Slider slider, float defaultVol)
        {
            float volume = PlayerPrefs.GetFloat(parameterName, defaultVol);
            slider.value = volume;

            _audioGameSettings.SetVolume(parameterName, volume);
        }

        private void OnMuteToggleChanged(bool isMuted)
        {
            if (isMuted)
            {
                _prevGeneralVolume = _generalSoundSlider.value;
                _prevMusicVolume = _musicSoundSlider.value;
                _prevEffectVolume = _effectSoundSlider.value;

                _audioGameSettings.SetVolume(_audioParams.AllSoundVolume, _minValue);
                _audioGameSettings.SetVolume(_audioParams.MusicVolume, _minValue);
                _audioGameSettings.SetVolume(_audioParams.EffectsVolume, _minValue);
            }
            else
            {
                _audioGameSettings.SetVolume(_audioParams.AllSoundVolume, _prevGeneralVolume);
                _audioGameSettings.SetVolume(_audioParams.MusicVolume, _prevMusicVolume);
                _audioGameSettings.SetVolume(_audioParams.EffectsVolume, _prevEffectVolume);
            }
        }
    }
}