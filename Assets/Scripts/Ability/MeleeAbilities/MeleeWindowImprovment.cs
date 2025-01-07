using Ability.MeleeAbilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeWindowImprovment : MonoBehaviour
{
    [SerializeField] private Image _abilityPanel;
    [SerializeField] private MeleeAbilityUser _user;

    private void OnEnable()
    {
        _user.LevelChanged += PressAbilityUpgrade;
        _user.BladeFuryUpgraded += CloseAbilityPanel;
        _user.BorrowedTimeIUpgraded += CloseAbilityPanel;
        _user.BloodlustIUpgraded += CloseAbilityPanel;
    }

    private void OnDisable()
    {
        _user.LevelChanged -= PressAbilityUpgrade;
        _user.BladeFuryUpgraded -= CloseAbilityPanel;
        _user.BorrowedTimeIUpgraded -= CloseAbilityPanel;
        _user.BloodlustIUpgraded -= CloseAbilityPanel;
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
