using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.MenuComponents
{
    public class PreGameMenu : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _returnToMainMenuButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayClick);
            _returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayClick);
            _returnToMainMenuButton.onClick.RemoveListener(ReturnToMainMenu);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void OnPlayClick()
        {
            SceneManager.LoadScene(1);
        }

        private void ReturnToMainMenu()
        {
            gameObject.SetActive(false);
            _mainMenu.gameObject.SetActive(true);
        }
    }
}