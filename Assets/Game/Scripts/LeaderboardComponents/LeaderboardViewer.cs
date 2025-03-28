using UnityEngine;
using DG.Tweening;
using Game.Scripts.MenuComponents.Panels;

namespace Game.Scripts.LeaderboardComponents
{
    public class LeaderboardViewer : PanelViewerBase
    {
        protected override void InitializePanel()
        {
            Panel.localScale = new Vector3(1, 0, 1);
        }

        protected override void ShowPanelAnimation()
        {
            Panel.DOScaleY(1, AnimationDuration).SetEase(EaseType);
        }

        protected override void HidePanelAnimation()
        {
            Panel.DOScaleY(0, AnimationDuration).SetEase(EaseType);
        }

        public void ToggleLeaderboardPanel()
        {
            TogglePanel();
        }
    }
}