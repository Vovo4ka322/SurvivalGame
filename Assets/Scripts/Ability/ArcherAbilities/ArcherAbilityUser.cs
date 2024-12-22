using MainPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAbilityUser : MonoBehaviour, IAbilityUser
{
    [SerializeField] private Player _player;
    [SerializeField] private MultishotUser _multishot;
    [SerializeField] private Multishot _multishotScriptableObject;

    private void Awake()
    {
        UpgradeFirstAbility();
    }

    public IAbilityUser Init() => this;

    public void OpenUpgraderWindow()
    {

    }

    public void UpgradeFirstAbility()
    {
        _multishot.Upgrade(_multishotScriptableObject);
    }

    public void UpgradeSecondAbility()
    {

    }

    public void UpgradeThirdAbility()
    {

    }

    public void UseFirstAbility()
    {
        StartCoroutine(_multishot.UseAbility());
    }

    public void UseSecondAbility()
    {
        _multishot.CalculateArrowFlight();
    }
}
