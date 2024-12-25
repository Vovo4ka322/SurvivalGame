using MainPlayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAbilityUser : MonoBehaviour, IAbilityUser
{
    [SerializeField] private Player _player;
    [SerializeField] private Bow _bow;
    [SerializeField] private MultishotUser _multishot;
    [SerializeField] private InsatiableHungerUser _insatiableHunger;

    [Header("Level of abilities")]
    [SerializeField] private RangeAbilityData _abilityDataFirstLevel;
    [SerializeField] private RangeAbilityData _abilityDataSecondLevel;
    [SerializeField] private RangeAbilityData _abilityDataThirdLevel;

    private int _firstLevel = 1;
    private int _secondLevel = 2;
    private int _thirdLevel = 3;

    private int _counterForInsatiableHunger = 0;
    private int _counterForMultishot = 0;
    private int _counterForBlur = 0;

    private int _firstUpgrade = 0;
    private int _secondUpgrade = 1;
    private int _thirdUpgrade = 2;

    private Dictionary<int, RangeAbilityData> _abilitiesDatas;

    public event Action LevelChanged;
    public event Action AbilityUpgraded;

    private void Awake()
    {
        _abilitiesDatas = new Dictionary<int, RangeAbilityData>
            {
                { _firstLevel, _abilityDataFirstLevel },
                { _secondLevel, _abilityDataSecondLevel },
                { _thirdLevel, _abilityDataThirdLevel }
            };

        _multishot.Upgrade(_abilitiesDatas[_firstLevel].Multishot);
        _insatiableHunger.Upgrade(_abilitiesDatas[_firstLevel].InsatiableHunger);
    }

    private void OnEnable()
    {
        _player.Level.LevelChanged += OpenUpgraderWindow;
    }

    private void OnDisable()
    {
        _player.Level.LevelChanged -= OpenUpgraderWindow;
    }

    public IAbilityUser Init() => this;

    public void OpenUpgraderWindow()
    {
        LevelChanged?.Invoke();
    }

    public void UpgradeFirstAbility()
    {
        if (IsTrue(_counterForMultishot, _firstUpgrade))
            UpgradeMultishot(_firstLevel);
        else if (IsTrue(_counterForMultishot, _secondUpgrade))
            UpgradeMultishot(_secondLevel);
        else if (IsTrue(_counterForMultishot, _thirdUpgrade))
            UpgradeMultishot(_thirdLevel);
    }

    public void UpgradeSecondAbility()
    {
        if (IsTrue(_counterForInsatiableHunger, _firstUpgrade))
            UpgradeInsatiableHunger(_firstLevel);
        else if (IsTrue(_counterForInsatiableHunger, _secondUpgrade))
            UpgradeInsatiableHunger(_secondLevel);
        else if (IsTrue(_counterForInsatiableHunger, _thirdUpgrade))
            UpgradeInsatiableHunger(_thirdLevel);
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
        StartCoroutine(_insatiableHunger.UseAbility(_player, _player));
    }

    private bool IsTrue(int counter, int numberOfUpgrade) => counter == numberOfUpgrade;

    private void UpgradeMultishot(int level)
    {
        _multishot.Upgrade(_abilitiesDatas[level].Multishot);
        _counterForMultishot++;
        AbilityUpgraded?.Invoke();
    }

    private void UpgradeInsatiableHunger(int level)
    {
        _insatiableHunger.Upgrade(_abilitiesDatas[level].InsatiableHunger);
        _counterForInsatiableHunger++;
        AbilityUpgraded?.Invoke();
    }

    private void UpgradeBlur(int level)
    {
        //_player.UpgradeCharacteristikByBloodlust(_abilitiesDatas[level].BloodlustScriptableObject);
        _counterForBlur++;
        AbilityUpgraded?.Invoke();
    }
}
