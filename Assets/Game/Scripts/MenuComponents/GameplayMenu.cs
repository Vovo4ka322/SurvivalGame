using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.MenuComponents
{
    public class GameplayMenu : MonoBehaviour
    {
        private const string Menu = nameof(Menu);

        [SerializeField] private Button _pauseButton;
        [SerializeField] private List<Button> _menuButton;
        [SerializeField] private Button _continuationButton;
        [SerializeField] private Image _pausePanel;
        [SerializeField] private Image _defeatPanel;

        [SerializeField] private Player _player;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(Pause);
            _continuationButton.onClick.AddListener(ContinueGame);

            for (int i = 0; i < _menuButton.Count; i++)
            {
                _menuButton[i].onClick.AddListener(ExitToMenu);
            }
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(Pause);
            _continuationButton.onClick.RemoveListener(ContinueGame);

            for (int i = 0; i < _menuButton.Count; i++)
            {
                _menuButton[i].onClick.RemoveListener(ExitToMenu);
            }

            _player.Death -= Lost;
        }

        public void Init(Player player)
        {
            _player = player;
            _player.Death += Lost;
        }

        private void ExitToMenu()
        {
            SceneManager.LoadScene(Menu);
            Time.timeScale = 1.0f;
        }

        private void Lost()
        {
            Time.timeScale = 0;
            _defeatPanel.gameObject.SetActive(true);
        }

        private void Pause()
        {
            Time.timeScale = 0;
            _pausePanel.gameObject.SetActive(true);
        }

        private void ContinueGame()
        {
            Time.timeScale = 1.0f;
            _pausePanel.gameObject.SetActive(false);
        }
    }
}