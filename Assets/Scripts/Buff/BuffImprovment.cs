using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffImprovment : MonoBehaviour
{
    [Header("BuffKeeperLevel")]
    [SerializeField] private BuffKeeper _buffKeeperFirstLevel;
    [SerializeField] private BuffKeeper _buffKeeperSecondLevel;
    [SerializeField] private BuffKeeper _buffKeeperThirdLevel;
    [SerializeField] private BuffKeeper _buffKeeperFourthLevel;
    [SerializeField] private BuffKeeper _buffKeeperFifthLevel;

    private int _firstLevel = 1;
    private int _secondLevel = 2;
    private int _thirdLevel = 3;
    private int _fourthLevel = 4;
    private int _fifthLevel = 5;

    private int _firstUpgrade = 0;
    private int _secondUpgrade = 1;
    private int _thirdUpgrade = 2;
    private int _fourthUpgrade = 3;
    private int _fifthUpgrade = 4;

    private int _counterForHealthBuff = 0;
    private int _counterForArmorBuff = 0;
    private int _counterForDamageBuff = 0;
    private int _counterForMovementSpeedBuff = 0;
    private int _counterForAttackSpeedBuff = 0;

    private Dictionary<int, BuffKeeper> _buffkeepers;

    public event Action HealthUpgraded;
    public event Action ArmorUpgraded;
    public event Action DamageUpgraded;
    public event Action AttackSpeedUpgraded;
    public event Action MovementSpeedUpgraded;

    public int MaxValue { get; private set; } = 5;

    public HealthBuff HealthBuff {  get; private set; }

    public ArmorBuff ArmorBuff { get; private set; }

    public DamageBuff DamageBuff { get; private set; }

    public MovementSpeedBuff MovementSpeedBuff { get; private set; }

    public AttackSpeedBuff AttackSpeedBuff { get; private set; }

    private void Awake()
    {
        _buffkeepers = new Dictionary<int, BuffKeeper>
        {
            { _firstLevel, _buffKeeperFirstLevel },
            { _secondLevel, _buffKeeperSecondLevel },
            { _thirdLevel, _buffKeeperThirdLevel },
            { _fourthLevel, _buffKeeperFourthLevel },
            { _fifthLevel, _buffKeeperFifthLevel },
        };
    }

    public void UpgradeHealth()
    {
        if (IsTrue(_counterForHealthBuff, _firstUpgrade))
            InitHealth(_firstLevel);
        else if(IsTrue(_counterForHealthBuff, _secondUpgrade))
            InitHealth(_secondLevel);
        else if (IsTrue(_counterForHealthBuff, _thirdUpgrade))
            InitHealth(_thirdLevel);
        else if (IsTrue(_counterForHealthBuff, _fourthUpgrade))
            InitHealth(_fourthLevel);
        else if (IsTrue(_counterForHealthBuff, _fifthUpgrade))
            InitHealth(_fifthLevel);
    }

    public void UpgradeArmor()
    {
        if (IsTrue(_counterForHealthBuff, _firstUpgrade))
            InitArmor(_firstLevel);
        else if (IsTrue(_counterForHealthBuff, _secondUpgrade))
            InitArmor(_secondLevel);
        else if (IsTrue(_counterForHealthBuff, _thirdUpgrade))
            InitArmor(_thirdLevel);
        else if (IsTrue(_counterForHealthBuff, _fourthUpgrade))
            InitArmor(_fourthLevel);
        else if (IsTrue(_counterForHealthBuff, _fifthUpgrade))
            InitArmor(_fifthLevel);
    }

    public void UpgradeDamage()
    {
        if (IsTrue(_counterForHealthBuff, _firstUpgrade))
            InitDamage(_firstLevel);
        else if (IsTrue(_counterForHealthBuff, _secondUpgrade))
            InitDamage(_secondLevel);
        else if (IsTrue(_counterForHealthBuff, _thirdUpgrade))
            InitDamage(_thirdLevel);
        else if (IsTrue(_counterForHealthBuff, _fourthUpgrade))
            InitDamage(_fourthLevel);
        else if (IsTrue(_counterForHealthBuff, _fifthUpgrade))
            InitDamage(_fifthLevel);
    }

    public void UpgradeAttackSpeed()
    {
        if (IsTrue(_counterForHealthBuff, _firstUpgrade))
            InitAttackSpeed(_firstLevel);
        else if (IsTrue(_counterForHealthBuff, _secondUpgrade))
            InitAttackSpeed(_secondLevel);
        else if (IsTrue(_counterForHealthBuff, _thirdUpgrade))
            InitAttackSpeed(_thirdLevel);
        else if (IsTrue(_counterForHealthBuff, _fourthUpgrade))
            InitAttackSpeed(_fourthLevel);
        else if (IsTrue(_counterForHealthBuff, _fifthUpgrade))
            InitAttackSpeed(_fifthLevel);
    }

    public void UpgradeMovementSpeed()
    {
        if (IsTrue(_counterForHealthBuff, _firstUpgrade))
            InitMovementSpeed(_firstLevel);
        else if (IsTrue(_counterForHealthBuff, _secondUpgrade))
            InitMovementSpeed(_secondLevel);
        else if (IsTrue(_counterForHealthBuff, _thirdUpgrade))
            InitMovementSpeed(_thirdLevel);
        else if (IsTrue(_counterForHealthBuff, _fourthUpgrade))
            InitMovementSpeed(_fourthLevel);
        else if (IsTrue(_counterForHealthBuff, _fifthUpgrade))
            InitMovementSpeed(_fifthLevel);
    }

    private void InitArmor(int level)
    {
        if (IsMaxValue(_counterForArmorBuff))
            return;

        ArmorBuff = _buffkeepers[level].ArmorBuffScriptableObject;
        _counterForArmorBuff++;
        ArmorUpgraded?.Invoke();
    }

    private void InitDamage(int level)
    {
        if (IsMaxValue(_counterForDamageBuff))
            return;

        DamageBuff = _buffkeepers[level].DamageBuffScriptableObject;
        _counterForDamageBuff++;
        DamageUpgraded?.Invoke();
    }

    private void InitMovementSpeed(int level)
    {
        if (IsMaxValue(_counterForMovementSpeedBuff))
            return;

        MovementSpeedBuff = _buffkeepers[level].MovementSpeedBuffScriptableObject;
        _counterForMovementSpeedBuff++;
        MovementSpeedUpgraded?.Invoke();
    }

    private void InitAttackSpeed(int level)
    {
        if (IsMaxValue(_counterForAttackSpeedBuff))
            return;

        AttackSpeedBuff = _buffkeepers[level].AttackSpeedBuffScriptableObject;
        _counterForAttackSpeedBuff++;
        AttackSpeedUpgraded?.Invoke();
    }

    private void InitHealth(int level)
    {
        if (IsMaxValue(_counterForHealthBuff))
            return;

        HealthBuff = _buffkeepers[level].HealthBuffScriptableObject;
        _counterForHealthBuff++;
        HealthUpgraded?.Invoke();
    }

    private bool IsMaxValue(int value) => value == MaxValue;

    private bool IsTrue(int counter, int numberOfUpgrade) => counter == numberOfUpgrade;
}