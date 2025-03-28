using Game.Scripts.PlayerComponents.Controller;
using UnityEngine;
using YG;

namespace Game.Scripts.MenuComponents
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private ManagementPhone _phoneControls;
        [SerializeField] private ManagementComputer _computerControls;

        private void Awake()
        {
            if (YandexGame.EnvironmentData.isDesktop)
            {
                _computerControls.gameObject.SetActive(true);
                _phoneControls.gameObject.SetActive(false);
            }
            else if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
            {
                _phoneControls.gameObject.SetActive(true);
                _computerControls.gameObject.SetActive(false);
            }
            else
            {
                _computerControls.gameObject.SetActive(true);
                _phoneControls.gameObject.SetActive(false);
            }
        }
    }
}