using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.Scripts.PlayerComponents;
using System.Collections.Generic;
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

        private Player _player;
        private bool _isPause;

        private void OnEnable()
        {
            if (_pauseButton != null)
            {
                _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            }

            if (_continueButton != null)
            {
                _continueButton.onClick.AddListener(OnContinueButtonClicked);
            }

            if (_menuBackButton != null)
            {
                for (int i = 0; i < _menuBackButton.Count; i++)
                {
                    _menuBackButton[i].onClick.AddListener(ExitToMenu);
                }
            }
        }

        private void OnDisable()
        {
            if (_pauseButton != null)
            {
                _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            }

            if (_continueButton != null)
            {
                _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            }

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

            _isPause = true;

            if (_isPause)
                Time.timeScale = 0;
        }

        private void OnContinueButtonClicked()
        {
            Time.timeScale = 1.0f;
            _pausePanel.gameObject.SetActive(false);
            _isPause = false;
        }
    }
}