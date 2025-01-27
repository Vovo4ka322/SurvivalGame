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

    private void OnHealthBuffUpgraded()
    {
        Upgrade(_healthBuffUpgraders, _healthBuffUpgraderCount);
    }

    private void OnArmorBuffUpgraded()
    {
        Upgrade(_armorBuffUpgraders, _armorBuffUpgraderCount);
    }

    private void Upgrade(List<Image> images, int index)
    {
        images[index].gameObject.SetActive(true);
    }
}
