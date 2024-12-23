using MainPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAbilityUser : MonoBehaviour, IAbilityUser
{
    [SerializeField] private Player _player;
    [SerializeField] private Bow _bow;
    [SerializeField] private MultishotUser _multishot;
    [SerializeField] private InsatiableHungerUser _insatiableHunger;
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
        StartCoroutine(_multishot.UseAbility(_bow));
    }

    public void UseSecondAbility()
    {
        _insatiableHunger.UseAbility(_player, _player);
    }
}
