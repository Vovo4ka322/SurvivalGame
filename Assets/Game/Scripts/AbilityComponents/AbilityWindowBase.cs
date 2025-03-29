using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents
{
    public abstract class AbilityWindowBase : MonoBehaviour
    {
        [SerializeField] protected Image _abilityPanel;
        
        public abstract void Init(Player player);
        
        protected abstract void SubscribeToEvents();
        
        protected abstract void UnsubscribeEvents();
        
        protected abstract void UpdateUpgradeTexts();
        
        protected void PressPlayerAbilityUpgrade()
        {
            UpdateUpgradeTexts();
            
            Time.timeScale = 0f;
            
            _abilityPanel.gameObject.SetActive(true);
        }
        
        protected void ClosePlayerAbilityPanel()
        {
            Time.timeScale = 1f;
            
            _abilityPanel.gameObject.SetActive(false);
        }
        
        protected virtual void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}