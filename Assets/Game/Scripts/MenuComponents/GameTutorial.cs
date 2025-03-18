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
        
        private Coroutine _hideCoroutine;
        private bool _isFinished = false;
        
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
        
        private void Start()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.gameObject.SetActive(true);
                
                _hideCoroutine = StartCoroutine(HideAfterDelay());
            }
        }
        
        public void Pause()
        {
            if (_hideCoroutine != null)
            {
                StopCoroutine(_hideCoroutine);
                _hideCoroutine = null;
            }
            
            if (_tutorialPanel != null)
            {
                _tutorialPanel.gameObject.SetActive(false);
            }
        }

        public void Resume()
        {
            if(_isFinished)
            {
                return;
            }
            
            if (_tutorialPanel != null)
            {
                _tutorialPanel.gameObject.SetActive(true);
                _hideCoroutine = StartCoroutine(HideAfterDelay());
            }
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_displayTime);
            yield return StartCoroutine(_tutorialPanel.FadeOut(_fadeDuration));
        }
        
        private void Continue()
        {
            _isFinished = true;
            Time.timeScale = 1;
            _tutorialPanel.gameObject.SetActive(false);
            _abilityInterface.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(true);
        }
    }
}