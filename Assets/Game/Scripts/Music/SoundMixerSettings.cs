using UnityEngine;
using UnityEngine.UI;

namespace Music
{
    public class SoundMixerSettings : MonoBehaviour
    {
        private const string AllVolumeParam = "AllSoundVolume";
        private const string MusicVolumeParam = "MusicVolume";
        private const string EffectsVolumeParam = "EffectsVolume";
        
        [SerializeField] private GameAudioPlayback _gameAudioPlayback;
        [SerializeField] private Slider _generalSoundSlider;
        [SerializeField] private Slider _musicSoundSlider;
        [SerializeField] private Slider _effectSoundSlider;
        [SerializeField] private Toggle _muteToggle;

        private readonly float _minValue = 0.0001f;
        private readonly float _defaultVolume = 0.75f;
        
        private float _prevGeneralVolume;
        private float _prevMusicVolume;
        private float _prevEffectVolume;

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            _muteToggle.onValueChanged.RemoveListener(OnMuteToggleChanged);
        }
        
        public void Initialize()
        {
            InitializeSlider(AllVolumeParam, _generalSoundSlider, _defaultVolume);
            InitializeSlider(MusicVolumeParam, _musicSoundSlider, _defaultVolume);
            InitializeSlider(EffectsVolumeParam, _effectSoundSlider, _defaultVolume);
            
            _generalSoundSlider.onValueChanged.AddListener(volume => _gameAudioPlayback.SetVolume(AllVolumeParam, volume));
            _musicSoundSlider.onValueChanged.AddListener(volume => _gameAudioPlayback.SetVolume(MusicVolumeParam, volume));
            _effectSoundSlider.onValueChanged.AddListener(volume => _gameAudioPlayback.SetVolume(EffectsVolumeParam, volume));
            
            _muteToggle.onValueChanged.AddListener(OnMuteToggleChanged);
        }
        
        private void InitializeSlider(string parameterName, Slider slider, float defaultVol)
        {
            float volume = PlayerPrefs.GetFloat(parameterName, defaultVol);
            slider.value = volume;
            _gameAudioPlayback.SetVolume(parameterName, volume);
        } 
        
        private void OnMuteToggleChanged(bool isMuted)
        {
            if (isMuted)
            {
                _prevGeneralVolume = _generalSoundSlider.value;
                _prevMusicVolume = _musicSoundSlider.value;
                _prevEffectVolume = _effectSoundSlider.value;
                
                _gameAudioPlayback.SetVolume(AllVolumeParam, _minValue);
                _gameAudioPlayback.SetVolume(MusicVolumeParam, _minValue);
                _gameAudioPlayback.SetVolume(EffectsVolumeParam, _minValue);
            }
            else
            {
                _gameAudioPlayback.SetVolume(AllVolumeParam, _prevGeneralVolume);
                _gameAudioPlayback.SetVolume(MusicVolumeParam, _prevMusicVolume);
                _gameAudioPlayback.SetVolume(EffectsVolumeParam, _prevEffectVolume);
            }
        }
    }
}