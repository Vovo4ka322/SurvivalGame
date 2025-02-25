using Ability.ArcherAbilities;
using PlayerComponents;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeAbilityViewer : MonoBehaviour
{
    private ArcherAbilityUser _archerAbilityUser;

    [Header("CooldownImages")]
    [SerializeField] private Image _firstAbility;
    [SerializeField] private Image _secondAbility;

    [Header("UpgradeImages")]
    [SerializeField] private List<Image> _firstAbilityImprovements;
    [SerializeField] private List<Image> _secondAbilityImprovements;
    [SerializeField] private List<Image> _thirdAbilityImprovements;

    private int _multishotImprovment = 0;
    private int _insatiableHungerImprovment = 0;
    private int _blurImprovment = 0;

    private void OnDisable()
    {
        _archerAbilityUser.MultishotUser.Used -= OnMultishotChanged;
        _archerAbilityUser.InsatiableHunger.Used -= OnInsatiableHungerChanged;
        _archerAbilityUser.MultishotUpgraded -= OnMultishotUpgraded;
        _archerAbilityUser.InsatiableHungerUpgraded -= OnInsatiableHungerUpgraded;
        _archerAbilityUser.BlurUpgraded -= OnBlurUpgraded;
    }

    public void Init(Player player)
    {
        _archerAbilityUser = player.GetComponentInChildren<ArcherAbilityUser>();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _archerAbilityUser.MultishotUser.Used += OnMultishotChanged;
        _archerAbilityUser.InsatiableHunger.Used += OnInsatiableHungerChanged;
        _archerAbilityUser.MultishotUpgraded += OnMultishotUpgraded;
        _archerAbilityUser.InsatiableHungerUpgraded += OnInsatiableHungerUpgraded;
        _archerAbilityUser.BlurUpgraded += OnBlurUpgraded;
    }

    private void OnMultishotChanged(float value)
    {
        Change(_firstAbility, _archerAbilityUser.MultishotUser.Multishot.CooldownTime, value);
    }

    private void OnInsatiableHungerChanged(float value)
    {
        Change(_secondAbility, _archerAbilityUser.InsatiableHunger.InsatiableHunger.CooldownTime, value);
    }

    private void Change(Image image, float cooldown, float value)
    {
        image.fillAmount = Mathf.InverseLerp(0, cooldown, value);
    }

    private void Upgrade(List<Image> images, int index)
    {
        images[index].gameObject.SetActive(true);
    }

    private void OnMultishotUpgraded()
    {
        if(_multishotImprovment == _archerAbilityUser.MaxValue)
            return;

        Upgrade(_firstAbilityImprovements, _multishotImprovment);
        _multishotImprovment++;
    }

    private void OnInsatiableHungerUpgraded()
    {
        if(_insatiableHungerImprovment == _archerAbilityUser.MaxValue)
            return;

        Upgrade(_secondAbilityImprovements, _insatiableHungerImprovment);
        _insatiableHungerImprovment++;
    }

    private void OnBlurUpgraded()
    {
        if (_blurImprovment == _archerAbilityUser.MaxValue)
            return;

        Upgrade(_thirdAbilityImprovements, _blurImprovment);
        _blurImprovment++;
    }
}
