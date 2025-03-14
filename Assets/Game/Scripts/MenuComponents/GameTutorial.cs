using System.Collections;
using UnityEngine;

namespace Game.Scripts.MenuComponents
{
    public class GameTutorial : MonoBehaviour
    {
        [SerializeField] private TutorialPanel _tutorialPanel;
        [SerializeField] private float _displayTime = 5f;
        [SerializeField] private float _fadeDuration = 1f;

        private void Start()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.gameObject.SetActive(true);
                
                StartCoroutine(HideAfterDelay());
            }
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_displayTime);
            yield return StartCoroutine(_tutorialPanel.FadeOut(_fadeDuration));
        }
    }
}