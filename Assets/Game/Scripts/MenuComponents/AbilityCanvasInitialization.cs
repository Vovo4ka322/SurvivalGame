using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.MenuComponents
{
    public abstract class AbilityCanvasInitialization <TAbilityInput> : MonoBehaviour where TAbilityInput : MonoBehaviour, IAbilityInput
    {
        [Header("Ability buttons")]
        [SerializeField] protected Button _firstAbilityUse;
        [SerializeField] protected Button _secondAbilityUse;
        [SerializeField] protected Button _firstUpgradeButton;
        [SerializeField] protected Button _secondUpgradeButton;
        [SerializeField] protected Button _thirdUpgradeButton;
    
        public void InitButtons(Player player)
        {
            TAbilityInput abilityInput = player.GetComponentInChildren<TAbilityInput>();
            abilityInput.Init(_firstAbilityUse, _secondAbilityUse, _firstUpgradeButton, _secondUpgradeButton, _thirdUpgradeButton);
        }
    }
}