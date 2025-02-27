using UnityEngine;
using UnityEngine.UI;
using Ability.MeleeAbilities;
using Game.Scripts.PlayerComponents;

public class MeleeWindowImprovment : MonoBehaviour
{
    [SerializeField] private Image _abilityPanel;

    private MeleeAbilityUser _meleeAbilityUser;

    private void OnDisable()
    {
        _meleeAbilityUser.LevelChanged -= PressAbilityUpgrade;
        _meleeAbilityUser.BladeFuryUpgraded -= CloseAbilityPanel;
        _meleeAbilityUser.BorrowedTimeIUpgraded -= CloseAbilityPanel;
        _meleeAbilityUser.BloodlustIUpgraded -= CloseAbilityPanel;
    }

    public void Init(Player player)
    {
        _meleeAbilityUser = player.GetComponentInChildren<MeleeAbilityUser>();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _meleeAbilityUser.LevelChanged += PressAbilityUpgrade;
        _meleeAbilityUser.BladeFuryUpgraded += CloseAbilityPanel;
        _meleeAbilityUser.BorrowedTimeIUpgraded += CloseAbilityPanel;
        _meleeAbilityUser.BloodlustIUpgraded += CloseAbilityPanel;
    }

    private void PressAbilityUpgrade()
    {
        Time.timeScale = 0f;
        _abilityPanel.gameObject.SetActive(true);
    }

    private void CloseAbilityPanel()
    {
        Time.timeScale = 1f;
        _abilityPanel.gameObject.SetActive(false);
    }
}