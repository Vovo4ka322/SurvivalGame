using Ability.MeleeAbilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeWindowImprovment : MonoBehaviour
{
    [SerializeField] private Image _abilityPanel;
    [SerializeField] private MeleeAbilityUser _player;

    private void OnEnable()
    {
        _player.LevelChanged += PressAbilityUpgrade;
        _player.BladeFuryUpgraded += CloseAbilityPanel;
        _player.BorrowedTimeIUpgraded += CloseAbilityPanel;
        _player.BloodlustIUpgraded += CloseAbilityPanel;
    }

    private void OnDisable()
    {
        _player.LevelChanged -= PressAbilityUpgrade;
        _player.BladeFuryUpgraded -= CloseAbilityPanel;
        _player.BorrowedTimeIUpgraded -= CloseAbilityPanel;
        _player.BloodlustIUpgraded -= CloseAbilityPanel;
    }

    public void PressAbilityUpgrade()
    {
        Time.timeScale = 0f;
        _abilityPanel.gameObject.SetActive(true);
    }

    public void CloseAbilityPanel()
    {
        Time.timeScale = 1f;
        _abilityPanel.gameObject.SetActive(false);
    }
}
