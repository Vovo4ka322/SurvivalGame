using Ability.ArcherAbilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeWindowImprovment : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private ArcherAbilityUser _user;

    private void OnEnable()
    {
        _user.LevelChanged += PressAbilityUpgrade;
        _user.MultishotUpgraded += CloseAbilityPanel;
        _user.InsatiableHungerUpgraded += CloseAbilityPanel;
        _user.BlurUpgraded += CloseAbilityPanel;
    }

    private void OnDisable()
    {
        _user.LevelChanged -= PressAbilityUpgrade;
        _user.MultishotUpgraded -= CloseAbilityPanel;
        _user.InsatiableHungerUpgraded -= CloseAbilityPanel;
        _user.BlurUpgraded -= CloseAbilityPanel;
    }

    public void PressAbilityUpgrade()
    {
        Time.timeScale = 0f;
        _image.gameObject.SetActive(true);
    }

    public void CloseAbilityPanel()
    {
        Time.timeScale = 1f;
        _image.gameObject.SetActive(false);
    }
}
