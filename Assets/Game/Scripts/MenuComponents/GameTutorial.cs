using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MenuComponents
{
    public class GameTutorial : MonoBehaviour
    {
        [SerializeField] private TutorialPanel _tutorialPanel;
        [SerializeField] private float _displayTime = 5f;
        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private Button _continueButton;
        [SerializeField] private RectTransform _abilityInterface;
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
            _continueButton.onClick.AddListener(Continue);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(Continue);
        }

        private void Continue()
        {
            Time.timeScale = 1;
            _tutorialPanel.gameObject.SetActive(false);
            _abilityInterface.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(true);
        }
    }
}