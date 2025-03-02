using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.MenuComponents
{
    public class GameplayMenu : MonoBehaviour
    {
        private const string MenuSceneName = "Menu";

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _menuBackButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Image _pausePanel;
        [SerializeField] private Image _defeatPanel;
        [SerializeField] private Text _defeatText;

        private Player _player;

        private void OnEnable()
        {
            if(_pauseButton != null)
            {
                _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            }

            if(_continueButton != null)
            {
                _continueButton.onClick.AddListener(OnContinueButtonClicked);
            }

            if (_menuBackButton != null)
            {
                _menuBackButton.onClick.AddListener(ExitToMenu);
            }
        }

        private void OnDisable()
        {
            if(_pauseButton != null)
            {
                _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            }

            if(_continueButton != null)
            {
                _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            }

            if(_menuBackButton != null)
            {
                _menuBackButton.onClick.AddListener(ExitToMenu);
            }

            if(_player != null)
            {
                _player.Death -= OnPlayerDeath;
            }
        }

        public void Init(Player player)
        {
            _player = player;

            if(_player != null)
            {
                _player.Death += OnPlayerDeath;
            }
        }

        private void ExitToMenu()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(MenuSceneName);
        }

        private void OnPlayerDeath()
        {
            Time.timeScale = 0;

            if (_defeatPanel != null)
            {
                _defeatPanel.gameObject.SetActive(true);
            }
            
            if (_continueButton != null)
            {
                _continueButton.interactable = false;
            }

            _continueButton.gameObject.SetActive(false);
            _defeatText.gameObject.SetActive(true);
        }
        
        private void OnPauseButtonClicked()
        {
            Time.timeScale = 0;
            
            if (_pausePanel != null)
            {
                _pausePanel.gameObject.SetActive(true);
            }
            
            if (_continueButton != null)
            {
                _continueButton.interactable = true;
            }
        }
        
        private void OnContinueButtonClicked()
        {
            Time.timeScale = 1.0f;
                
            if (_pausePanel != null)
            {
                _pausePanel.gameObject.SetActive(false);
            }
        }
    }
}