using UnityEngine;
using UnityEngine.UI;
using Music;

namespace MenuComponents
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private Button _returnToMainMenuButton;
        [SerializeField] private SoundMixerSettings _soundMixerSettings;
        
        private void OnEnable()
        {
            _returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
        
        private void OnDisable()
        {
            _returnToMainMenuButton.onClick.RemoveListener(ReturnToMainMenu);
        }
        
        private void Start()
        {
            _soundMixerSettings.Initialize();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        private void ReturnToMainMenu()
        {
            gameObject.SetActive(false);
            _mainMenu.gameObject.SetActive(true);
        }
    }
}