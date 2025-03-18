using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.Scripts.EnemyComponents;
using Game.Scripts.HealthComponents;
using Game.Scripts.MusicComponents;
using Game.Scripts.PlayerComponents;
using YG;

namespace Game.Scripts.MenuComponents
{
    public class GameplayMenu : MonoBehaviour
    {
        private const string MenuSceneName = "Menu";

        [SerializeField] private Button _pauseButton;
        [SerializeField] private List<Button> _menuBackButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Image _pausePanel;
        [SerializeField] private Image _defeatPanel;
        [SerializeField] private Image _victoryPanel;
        [SerializeField] private BossHealthViewer _bossHealthViewer;
        [SerializeField] private WaveTimerUI _waveTimerUI;
        [SerializeField] private GameTutorial _tutorialPanel;
        [SerializeField] private PlayerPanel _playerPanel;
        
        [Header("Audio Settings")]
        [SerializeField] private AudioParameterNames _audioParams;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _effectsVolumeSlider;
        
        private AudioGameSettings _audioGameSettings;
        private GameSceneAudio _gameSceneAudio;
        private Player _player;
        
        public BossHealthViewer BossHealthViewer => _bossHealthViewer;
        public WaveTimerUI WaveTimerUI => _waveTimerUI;
        
        private void OnEnable()
        {
            _pauseButton?.onClick.AddListener(OnPauseButtonClicked);
            _continueButton?.onClick.AddListener(OnContinueButtonClicked);

            if (_menuBackButton != null)
            {
                for (int i = 0; i < _menuBackButton.Count; i++)
                {
                    _menuBackButton[i].onClick.AddListener(ExitToMenu);
                }
            }
            
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            _effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
        }

        private void OnDisable()
        {
            _pauseButton?.onClick.RemoveListener(OnPauseButtonClicked);
            _continueButton?.onClick.RemoveListener(OnContinueButtonClicked);
            
            if (_menuBackButton != null)
            {
                for (int i = 0; i < _menuBackButton.Count; i++)
                {
                    _menuBackButton[i].onClick.RemoveListener(ExitToMenu);
                }
            }

            if (_player != null)
            {
                _player.Death -= OnPlayerDeath;
            }
            
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            _effectsVolumeSlider.onValueChanged.RemoveListener(OnEffectsVolumeChanged);
        }
        
        public void SetGameSceneAudio(GameSceneAudio sceneAudio)
        {
            _gameSceneAudio = sceneAudio;
        }
        
        public void SetAudioGameSettings(AudioGameSettings audioSettings)
        {
            _audioGameSettings = audioSettings;
        }

        public void Init(Player player)
        {
            _player = player;

            if (_player != null)
            {
                _player.Death += OnPlayerDeath;
            }
        }

        public void OnPlayerWon()
        {
            Time.timeScale = 0;
            
            _victoryPanel.gameObject.SetActive(true);
        }

        private void CallAd()
        {
            YandexGame.FullscreenShow();
        }

        private void ExitToMenu()
        {
            if (_gameSceneAudio != null)
            {
                _gameSceneAudio.StopAllMusic();
                
                Destroy(_gameSceneAudio.gameObject);
            }
            
            Time.timeScale = 1.0f;
            
            SceneManager.LoadScene(MenuSceneName);
        }

        private void OnPlayerDeath()
        {
            CallAd();
            
            Time.timeScale = 0;

            _defeatPanel.gameObject.SetActive(true);
            
            _continueButton.interactable = false;
        }

        private void OnPauseButtonClicked()
        {
            CallAd();

            _pausePanel.gameObject.SetActive(true);
            _continueButton.interactable = true;
            
            Time.timeScale = 0;
            
            _musicVolumeSlider.gameObject.SetActive(true);
            _effectsVolumeSlider.gameObject.SetActive(true);
            
            _musicVolumeSlider.value = PlayerPrefs.GetFloat(_audioParams.MusicVolume, 0.75f);
            _effectsVolumeSlider.value = PlayerPrefs.GetFloat(_audioParams.EffectsVolume, 0.75f);

            if(_bossHealthViewer != null && _bossHealthViewer.Boss != null)
            {
                _bossHealthViewer?.gameObject.SetActive(false);
            }

            _tutorialPanel?.Pause();
            _pauseButton?.gameObject.SetActive(false);
            _waveTimerUI?.gameObject.SetActive(false);
            _playerPanel?.gameObject.SetActive(false);
        }

        private void OnContinueButtonClicked()
        {
            Time.timeScale = 1.0f;
            
            _pausePanel.gameObject.SetActive(false);
            
            _musicVolumeSlider.gameObject.SetActive(false);
            _effectsVolumeSlider.gameObject.SetActive(false);

            if(_bossHealthViewer != null && _bossHealthViewer.Boss != null)
            {
                _bossHealthViewer.gameObject.SetActive(true);
            }

            _tutorialPanel?.Resume();
            _pauseButton?.gameObject.SetActive(true);
            _waveTimerUI?.gameObject.SetActive(true);
            _playerPanel?.gameObject.SetActive(true);
        }
        
        private void OnMusicVolumeChanged(float value)
        {
            _audioGameSettings.SetVolume(_audioParams.MusicVolume, value);
        }

        private void OnEffectsVolumeChanged(float value)
        {
            _audioGameSettings.SetVolume(_audioParams.EffectsVolume, value);
        }
    }
}