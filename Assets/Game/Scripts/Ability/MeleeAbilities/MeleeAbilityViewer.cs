using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ability.MeleeAbilities;
using Game.Scripts.PlayerComponents;

public class MeleeAbilityViewer : MonoBehaviour
{
    [Header("Cooldown Images")]
    [SerializeField] private Image _firstAbility;
    [SerializeField] private Image _secondAbility;

    [Header("Upgrade Images")]
    [SerializeField] private List<Image> _firstAbilityImprovements;
    [SerializeField] private List<Image> _secondAbilityImprovements;
    [SerializeField] private List<Image> _thirdAbilityImprovements;

    private MeleeAbilityUser _meleeAbilityUser;

    private int _bladeFuryImprovment = 0;
    private int _borrowedTimeImprovment = 0;
    private int _bloodlustImprovment = 0;

    private void OnDisable()
    {
        _meleeAbilityUser.BladeFury.Used -= OnBladeFuryChanged;
        _meleeAbilityUser.BorrowedTime.Used -= OnBorrowedTimeChanged;
        _meleeAbilityUser.BladeFuryUpgraded -= OnBladeFuryUpgraded;
        _meleeAbilityUser.BorrowedTimeIUpgraded -= OnBorrowedTimeUpgraded;
        _meleeAbilityUser.BloodlustIUpgraded -= OnBloodlustUpgraded;
    }

    public void Init(Player player)
    {
        _meleeAbilityUser = player.GetComponentInChildren<MeleeAbilityUser>();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _meleeAbilityUser.BladeFury.Used += OnBladeFuryChanged;
        _meleeAbilityUser.BorrowedTime.Used += OnBorrowedTimeChanged;
        _meleeAbilityUser.BladeFuryUpgraded += OnBladeFuryUpgraded;
        _meleeAbilityUser.BorrowedTimeIUpgraded += OnBorrowedTimeUpgraded;
        _meleeAbilityUser.BloodlustIUpgraded += OnBloodlustUpgraded;
    }

    private void OnBladeFuryChanged(float value)
    {
        Change(_firstAbility, _meleeAbilityUser.BladeFury.BladeFury.CooldownTime, value);
    }

    private void OnBorrowedTimeChanged(float value)
    {
        Change(_secondAbility, _meleeAbilityUser.BorrowedTime.BorrowedTime.CooldownTime, value);
    }

    private void Change(Image image, float cooldown, float value)
    {
        image.fillAmount = Mathf.InverseLerp(0, cooldown, value);
    }

    private void Upgrade(List<Image> images, int index)
    {
        images[index].gameObject.SetActive(true);
    }

    private void OnBladeFuryUpgraded()
    {
        if (_bladeFuryImprovment == _meleeAbilityUser.MaxValue)
            return;

        Upgrade(_firstAbilityImprovements, _bladeFuryImprovment);
        _bladeFuryImprovment++;
    }

    private void OnBorrowedTimeUpgraded()
    {
        if (_borrowedTimeImprovment == _meleeAbilityUser.MaxValue)
            return;

        Upgrade(_secondAbilityImprovements, _borrowedTimeImprovment);
        _borrowedTimeImprovment++;
    }

    private void OnBloodlustUpgraded()
    {
        if (_bloodlustImprovment == _meleeAbilityUser.MaxValue)
            return;

        Upgrade(_thirdAbilityImprovements, _bloodlustImprovment);
        _bloodlustImprovment++;
    }
}
