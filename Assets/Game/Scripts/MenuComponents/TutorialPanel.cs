using System.Collections;
using UnityEngine;

namespace Game.Scripts.MenuComponents
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public IEnumerator FadeOut(float duration)
        {
            float elapsed = 0f;
            _canvasGroup.alpha = 1f;
            
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                _canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
                
                yield return null;
            }
            
            _canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }
    }
}