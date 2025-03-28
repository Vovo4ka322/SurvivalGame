using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.AbilityComponents.MeleeAbilities;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.MenuComponents
{
    public class MeleeCanvasInitialization : MonoBehaviour
    {
        [Header("Melee buttons")]
        [SerializeField] private Button _firstMeleeAbilityUse;
        [SerializeField] private Button _secondMeleeAbilityUse;
        [SerializeField] private Button _firstMeleeUpgradeButton;
        [SerializeField] private Button _secondMeleeUpgradeButton;
        [SerializeField] private Button _thirdMeleeUpgradeButton;

        public void InitButtons(Player player)
        {
            player.GetComponentInChildren<MeleeAbilityInput>().Init(
                _firstMeleeAbilityUse,
                _secondMeleeAbilityUse,
                _firstMeleeUpgradeButton,
                _secondMeleeUpgradeButton,
                _thirdMeleeUpgradeButton);
        }
    }
}