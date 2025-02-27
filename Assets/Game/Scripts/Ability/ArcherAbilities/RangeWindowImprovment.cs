using UnityEngine;
using UnityEngine.UI;
using Ability.ArcherAbilities;
using Game.Scripts.PlayerComponents;

public class RangeWindowImprovment : MonoBehaviour
{
    [SerializeField] private Image _image;

    private ArcherAbilityUser _archerAbilityUser;

    private void OnDisable()
    {
        _archerAbilityUser.LevelChanged -= PressAbilityUpgrade;
        _archerAbilityUser.MultishotUpgraded -= CloseAbilityPanel;
        _archerAbilityUser.InsatiableHungerUpgraded -= CloseAbilityPanel;
        _archerAbilityUser.BlurUpgraded -= CloseAbilityPanel;
    }

    public void Init(Player player)
    {
        _archerAbilityUser = player.GetComponentInChildren<ArcherAbilityUser>();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _archerAbilityUser.LevelChanged += PressAbilityUpgrade;
        _archerAbilityUser.MultishotUpgraded += CloseAbilityPanel;
        _archerAbilityUser.InsatiableHungerUpgraded += CloseAbilityPanel;
        _archerAbilityUser.BlurUpgraded += CloseAbilityPanel;
    }

    private void PressAbilityUpgrade()
    {
        Time.timeScale = 0f;
        _image.gameObject.SetActive(true);
    }

    private void CloseAbilityPanel()
    {
        Time.timeScale = 1f;
        _image.gameObject.SetActive(false);
    }
}
