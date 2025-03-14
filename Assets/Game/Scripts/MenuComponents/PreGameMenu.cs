using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.Scripts.MusicComponents;
using YG;

namespace Game.Scripts.MenuComponents
{
    public class PreGameMenu : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _returnToMainMenuButton;
        [SerializeField] private GameAudioPlayback _mainMenuAudio;
        
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
            YandexGame.FullscreenShow();

            if(_mainMenuAudio != null)
            {
                _mainMenuAudio.StopBackgroundMusic();
                Destroy(_mainMenuAudio.gameObject); 
            }
            
            SceneManager.LoadScene(1);
        }

        private void ReturnToMainMenu()
        {
            gameObject.SetActive(false);
            _mainMenu.gameObject.SetActive(true);
        }
    }
}