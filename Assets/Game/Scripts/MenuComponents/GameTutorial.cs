using System.Collections;
using UnityEngine;

namespace Game.Scripts.MenuComponents
{
    public class GameTutorial : MonoBehaviour
    {
        [SerializeField] private TutorialPanel _tutorialPanel;
        [SerializeField] private float _displayTime = 5f;
        [SerializeField] private float _fadeDuration = 1f;
        
        private readonly bool _isFinished = false;
        
        private Coroutine _hideCoroutine;
        
        private void Start()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.gameObject.SetActive(true);
                
                StartCoroutine(HideAfterDelay());
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
    }
}