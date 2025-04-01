using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.MenuComponents.Panels
{
    public class GameTutorial : MonoBehaviour
    {
        [SerializeField] private TutorialPanel _tutorialPanel;
        [SerializeField] private RectTransform _abilityInterface;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _pauseButton;

        private void Awake()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueClicked);
        }

        private void OnContinueClicked()
        {
            YandexGame.FullscreenShow();

            Time.timeScale = 1;
            _tutorialPanel.gameObject.SetActive(false);
            _abilityInterface.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(true);
        }
    }
}