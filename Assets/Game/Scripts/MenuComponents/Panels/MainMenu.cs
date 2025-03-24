using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.MusicComponents;

namespace Game.Scripts.MenuComponents.Panels
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private PreGameMenu _preGameMenu;
        [SerializeField] private SettingsMenu _settingsMenu;
        [SerializeField] private Button _choicePlayerButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GameAudioPlayback _gameAudioPlayback;

        private void Awake()
        {
            _menuCanvas.gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            _choicePlayerButton.onClick.AddListener(OpenPreGameMenu);
            _settingsButton.onClick.AddListener(OpenSettingsMenu);
        }
        
        private void OnDisable()
        {
            _choicePlayerButton.onClick.RemoveListener(OpenPreGameMenu);
            _settingsButton.onClick.RemoveListener(OpenSettingsMenu);
        }

        private void Start()
        {
            if(_gameAudioPlayback != null)
            {
                _gameAudioPlayback.PlayBackgroundMusic();
            }
        }
        
        private void OpenPreGameMenu()
        {
            gameObject.SetActive(false);
            _preGameMenu.Show();
        }
        
        private void OpenSettingsMenu()
        {
            gameObject.SetActive(false);
            _settingsMenu.Show();
        }
    }
}