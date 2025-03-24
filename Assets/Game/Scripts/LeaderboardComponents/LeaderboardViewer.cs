using UnityEngine;
using DG.Tweening;

namespace Game.Scripts.LeaderboardComponents
{
    public class LeaderboardViewer : MonoBehaviour
    {
        [Header("Setting animations")]
        [SerializeField] private RectTransform _leaderBoardPanel;
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private Ease _easeType = Ease.OutCubic;
        
        private bool _isShown = false;
        
        private void Awake()
        {
            _leaderBoardPanel.localScale = new Vector3(1, 0, 1);
        }
        
        public void OnToggleButtonClick()
        {
            if (_isShown)
            {
                HideLeaderBoard();
            }
            else
            {
                ShowLeaderBoard();
            }
        }
        
        private void ShowLeaderBoard()
        {
            _leaderBoardPanel.DOScaleY(1, _animationDuration).SetEase(_easeType);

            _isShown = true;
        }

        private void HideLeaderBoard()
        {
            _leaderBoardPanel.DOScaleY(0, _animationDuration).SetEase(_easeType);

            _isShown = false;
        }
    }
}