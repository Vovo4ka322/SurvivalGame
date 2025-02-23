using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuComponents
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _choicePlayerButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _returnToMainMenuButton;
        [SerializeField] private GameObject _shop;
        [SerializeField] private GameObject _menu;

        private void OnEnable()
        {
            _choicePlayerButton.onClick.AddListener(OnChoosePlayerClick);
            _playButton.onClick.AddListener(OnPlayClick);
            _returnToMainMenuButton.onClick.AddListener(OnBackToMenuClick);
        }

        private void OnDisable()
        {
            _choicePlayerButton?.onClick.RemoveListener(OnChoosePlayerClick);
            _playButton?.onClick.RemoveListener(OnPlayClick);
            _returnToMainMenuButton.onClick.RemoveListener(OnBackToMenuClick);
        }

        private void OnChoosePlayerClick()
        {
            _menu.gameObject.SetActive(false);
            _shop.gameObject.SetActive(true);
        }

        private void OnPlayClick()
        {
            SceneManager.LoadScene(0);
        }

        private void OnBackToMenuClick()
        {
            _menu.gameObject.SetActive(true);
            _shop.gameObject.SetActive(false);
        }
    }
}