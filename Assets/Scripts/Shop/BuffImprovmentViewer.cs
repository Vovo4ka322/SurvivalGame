using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffImprovmentViewer: MonoBehaviour
{
    [SerializeField] private BuffShop _buffShop;

    [SerializeField] private List<Image> _healthBuffUpgraders;
    [SerializeField] private List<Image> _armorBuffUpgraders;
    [SerializeField] private List<Image> _damageBuffUpgraders;
    [SerializeField] private List<Image> _attackSpeedBuffUpgraders;
    [SerializeField] private List<Image> _movementSpeedBuffUpgraders;

    private int _healthBuffUpgraderCount = 0;
    private int _armorBuffUpgraderCount = 0;
    private int _damageBuffUpgraderCount = 0;
    private int _attackSpeedBuffUpgraderCount = 0;
    private int _movementSpeedBuffUpgraderCount = 0;

    private void OnEnable()
    {
        _buffShop.HealthUpgraded += OnHealthBuffUpgraded;
        _buffShop.ArmorUpgraded += OnArmorBuffUpgraded;
        _buffShop.DamageUpgraded += OnDamageBuffUpgraded;
        _buffShop.AttackSpeedUpgraded += OnAttackSpeedBuffUpgraded;
        _buffShop.MovementSpeedUpgraded += OnMovementSpeedBuffUpgraded;
    }

    private void OnDisable()
    {
        _buffShop.HealthUpgraded -= OnHealthBuffUpgraded;
        _buffShop.ArmorUpgraded -= OnArmorBuffUpgraded;
        _buffShop.DamageUpgraded -= OnDamageBuffUpgraded;
        _buffShop.AttackSpeedUpgraded -= OnAttackSpeedBuffUpgraded;
        _buffShop.MovementSpeedUpgraded -= OnMovementSpeedBuffUpgraded;
    }

    private void OnHealthBuffUpgraded()
    {
        if(IsFull(_healthBuffUpgraderCount))
            return;

        Upgrade(_healthBuffUpgraders, _healthBuffUpgraderCount);
        _healthBuffUpgraderCount++;
    }

    private void OnArmorBuffUpgraded()
    {
        if(IsFull(_armorBuffUpgraderCount)) 
            return;

        Upgrade(_armorBuffUpgraders, _armorBuffUpgraderCount);
        _armorBuffUpgraderCount++;
    }

    private void OnDamageBuffUpgraded()
    {
        if (IsFull(_damageBuffUpgraderCount)) 
            return;

        Upgrade(_damageBuffUpgraders, _damageBuffUpgraderCount);
        _damageBuffUpgraderCount++;
    }

    private void OnAttackSpeedBuffUpgraded()
    {
        if( IsFull(_attackSpeedBuffUpgraderCount))
            return;

        Upgrade(_attackSpeedBuffUpgraders, _attackSpeedBuffUpgraderCount);
        _attackSpeedBuffUpgraderCount++;
    }

    private void OnMovementSpeedBuffUpgraded()
    {
        if(IsFull(_movementSpeedBuffUpgraderCount))
            return;

        Upgrade(_movementSpeedBuffUpgraders, _movementSpeedBuffUpgraderCount);
        _movementSpeedBuffUpgraderCount++;
    }

    private bool IsFull(int value) => _buffShop.MaxCount == value;

    private void Upgrade(List<Image> images, int index)
    {
        images[index].gameObject.SetActive(true);
    }
}
