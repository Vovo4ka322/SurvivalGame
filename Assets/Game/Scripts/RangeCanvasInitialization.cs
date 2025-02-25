using PlayerComponents;
using UnityEngine;
using UnityEngine.UI;

public class RangeCanvasInitialization : MonoBehaviour
{
    [Header("Range buttons")]
    [SerializeField] private Button _firstMeleeAbilityUse;
    [SerializeField] private Button _secondMeleeAbilityUse;
    [SerializeField] private Button _firstMeleeUpgradeButton;
    [SerializeField] private Button _secondMeleeUpgradeButton;
    [SerializeField] private Button _thirdMeleeUpgradeButton;

    public void InitButtons(Player player)
    {
        player.GetComponentInChildren<RangeAbilityInput>().Init(_firstMeleeAbilityUse, _secondMeleeUpgradeButton, 
            _firstMeleeUpgradeButton, _secondMeleeUpgradeButton, _thirdMeleeUpgradeButton);
    }
}
