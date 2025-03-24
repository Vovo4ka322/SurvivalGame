using UnityEngine;
using DG.Tweening;

namespace Game.Scripts.MenuComponents.Panels
{
    public class DifficultyPanelViewer : PanelViewerBase
    {
        protected override void InitializePanel()
        {
            Panel.localScale = Vector3.zero;
        }

        protected override void ShowPanelAnimation()
        {
            Panel.DOScale(Vector3.one, AnimationDuration).SetEase(EaseType);
        }

        protected override void HidePanelAnimation()
        {
            Panel.DOScale(Vector3.zero, AnimationDuration).SetEase(EaseType);
        }
        
        public void ToggleDifficultyPanel()
        {
            TogglePanel();
        }
    }
}