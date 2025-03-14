using System.Collections;
using UnityEngine;

namespace Game.Scripts.MenuComponents
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public IEnumerator FadeOut(float duration)
        {
            float startAlpha = _canvasGroup.alpha;
            float time = 0f;
            
            while (time < duration)
            {
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / duration);
                time += Time.deltaTime;
                
                yield return null;
            }
            
            _canvasGroup.alpha = 0f;
            
            gameObject.SetActive(false);
        }
    }
}