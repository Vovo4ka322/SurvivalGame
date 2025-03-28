using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.MenuComponents.Panels
{
    public abstract class PanelViewerBase : MonoBehaviour
    {
        [Header("Setting animations")]
        [SerializeField] private RectTransform _panel;
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private Ease _easeType = Ease.OutCubic;

        private bool _isPanelVisible = false;

        public RectTransform Panel => _panel;
        public float AnimationDuration => _animationDuration;
        public Ease EaseType => _easeType;

        protected virtual void Awake()
        {
            InitializePanel();
        }

        protected abstract void InitializePanel();

        protected abstract void ShowPanelAnimation();

        protected abstract void HidePanelAnimation();

        protected void TogglePanel()
        {
            if (_isPanelVisible)
            {
                HidePanel();
            }
            else
            {
                ShowPanel();
            }
        }

        private void ShowPanel()
        {
            ShowPanelAnimation();
            _isPanelVisible = true;
        }

        private void HidePanel()
        {
            HidePanelAnimation();
            _isPanelVisible = false;
        }
    }
}