using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MenuComponents.Panels
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private Button _returnToMainMenuButton;
        
        private void OnEnable()
        {
            _returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
        
        private void OnDisable()
        {
            _returnToMainMenuButton.onClick.RemoveListener(ReturnToMainMenu);
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