using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.AbilityComponents;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.MenuComponents
{
    public class AbilityButtonsInitializer : MonoBehaviour 
    {
        [Header("Ability buttons")]
        [SerializeField] private Button _firstAbilityUse;
        [SerializeField] private Button _secondAbilityUse;
        [SerializeField] private Button _firstUpgradeButton;
        [SerializeField] private Button _secondUpgradeButton;
        [SerializeField] private Button _thirdUpgradeButton;
    
        public void InitButtons(Player player)
        {
            AbilityInput abilityInput = player.GetComponentInChildren<AbilityInput>();
            
            if (abilityInput != null)
            {
                abilityInput.Init(_firstAbilityUse, _secondAbilityUse, _firstUpgradeButton, _secondUpgradeButton, _thirdUpgradeButton);
            }
        }
    }
}
