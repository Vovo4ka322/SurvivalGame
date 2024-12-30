using Ability.ArcherAbilities;
using Ability.MeleeAbilities;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private ArcherAbilityUser _user;//временно тут

    private void OnEnable()
    {
        _user.LevelChanged += PressAbilityUpgrade;
        _user.AbilityUpgraded += CloseAbilityPanel;
    }

    private void OnDisable()
    {
        _user.LevelChanged -= PressAbilityUpgrade;
        _user.AbilityUpgraded -= CloseAbilityPanel;
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