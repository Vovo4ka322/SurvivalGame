using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffShop : MonoBehaviour
{
    [SerializeField] private BuffImprovment _buffImprovment;
    [SerializeField] private Image _buffPanel;
    [SerializeField] private Button _openerPanelButton;
    [SerializeField] private Button _leaverPanelButton;

    [Header("Buffs buttons")]
    [SerializeField] private Button _healthBuffButton;
    [SerializeField] private Button _armorBuffButton;
    [SerializeField] private Button _damageBuffButton;
    [SerializeField] private Button _attackSpeedBuffButton;
    [SerializeField] private Button _movementSpeedBuffButton;

    [Header("Text and button of purchase of every buffs buttons")]
    [SerializeField] private Button _healthBuffPurchaseButton;
    [SerializeField] private TextMeshProUGUI _healthBuffDescription;
    [SerializeField] private Button _armorBuffPurchaseButton;
    [SerializeField] private TextMeshProUGUI _armorBuffDescription;
    [SerializeField] private Button _damageBuffPurchaseButton;
    [SerializeField] private TextMeshProUGUI _damageBuffDescription;
    [SerializeField] private Button _attackSpeedBuffPurchaseButton;
    [SerializeField] private TextMeshProUGUI _attackSpeedBuffDescription;
    [SerializeField] private Button _movementSpeedBuffPurchaseButton;
    [SerializeField] private TextMeshProUGUI _movementSpeedBuffDescription;

    [field: SerializeField] public int HealthBuffPrice {  get; private set; }

    [field: SerializeField] public int ArmorBuffPrice { get; private set; }

    [field: SerializeField] public int DamageBuffPrice { get; private set; }

    [field: SerializeField] public int AttackSpeedBuffPrice { get; private set; }

    [field: SerializeField] public int MovementSpeedBuffPrice { get; private set; }


    private Wallet _wallet;
    private IDataProvider _dataProvider;
    private PlayerCharacteristicData _calculationFinalValue;

    private int _healthBuffCounter;
    private int _armorBuffCounter;
    private int _damageBuffCounter;
    private int _attackSpeedBuffCounter;
    private int _movementSpeedBuffCounter;

    private int _healthKey = 0;
    private int _armorKey = 1;
    private int _damageKey = 2;
    private int _attackSpeedKey = 3;
    private int _movementSpeedKey = 4;

    private Dictionary<int, Button> _purchaseButtons;
    private Dictionary<int, TextMeshProUGUI> _purchaseTexts;

    public event Action HealthUpgraded;
    public event Action ArmorUpgraded;
    public event Action DamageUpgraded;
    public event Action AttackSpeedUpgraded;
    public event Action MovementSpeedUpgraded;

    public int MaxCount { get; private set; } = 5;

    private void OnEnable()
    {
        _healthBuffButton.onClick.AddListener(OnHealthBuffClick);
        _armorBuffButton.onClick.AddListener(OnArmorBuffClick);
        _damageBuffButton.onClick.AddListener(OnDamageBuffClick);
        _attackSpeedBuffButton.onClick.AddListener(OnAttackSpeedBuffClick);
        _movementSpeedBuffButton.onClick.AddListener(OnMovementSpeedBuffClick);

        _damageBuffPurchaseButton.onClick.AddListener(OnBuyDamageBuff);
        _healthBuffPurchaseButton.onClick.AddListener(OnBuyHealthBuff);
        _armorBuffPurchaseButton.onClick.AddListener(OnBuyArmorBuff);
        _attackSpeedBuffPurchaseButton.onClick.AddListener(OnBuyAttackSpeedBuff);
        _movementSpeedBuffPurchaseButton.onClick.AddListener(OnBuyMovementSpeedBuff);

        _openerPanelButton.onClick.AddListener(OnBuffPanelOpened);
        _leaverPanelButton.onClick.AddListener(OnBuffPanelClosed);
    }

    private void OnDisable()
    {
        _healthBuffButton.onClick.RemoveListener(OnHealthBuffClick);
        _armorBuffButton.onClick.RemoveListener(OnArmorBuffClick);
        _damageBuffButton.onClick.RemoveListener(OnDamageBuffClick);
        _attackSpeedBuffButton.onClick.RemoveListener(OnAttackSpeedBuffClick);
        _movementSpeedBuffButton.onClick.RemoveListener(OnMovementSpeedBuffClick);

        _damageBuffPurchaseButton.onClick.RemoveListener(OnBuyDamageBuff);
        _healthBuffPurchaseButton.onClick.RemoveListener(OnBuyHealthBuff);
        _armorBuffPurchaseButton.onClick.RemoveListener(OnBuyArmorBuff);
        _attackSpeedBuffPurchaseButton.onClick.RemoveListener(OnBuyAttackSpeedBuff);
        _movementSpeedBuffPurchaseButton.onClick.RemoveListener(OnBuyMovementSpeedBuff);

        _openerPanelButton.onClick.RemoveListener(OnBuffPanelOpened);
        _leaverPanelButton.onClick.RemoveListener(OnBuffPanelClosed);
    }

    public void Init(Wallet wallet, IDataProvider dataProvider, IPersistentData persistentData)
    {
        _wallet = wallet;
        _dataProvider = dataProvider;

        _calculationFinalValue = persistentData.PlayerData.CalculationFinalValue;

        _purchaseButtons = new()
        {
            { _healthKey, _healthBuffPurchaseButton },
            { _armorKey, _armorBuffPurchaseButton },
            { _damageKey, _damageBuffPurchaseButton },
            { _attackSpeedKey, _attackSpeedBuffPurchaseButton },
            { _movementSpeedKey, _movementSpeedBuffPurchaseButton }
        };

        _purchaseTexts = new()
        {
            { _healthKey, _healthBuffDescription },
            { _armorKey, _armorBuffDescription },
            { _damageKey, _damageBuffDescription },
            { _attackSpeedKey, _attackSpeedBuffDescription },
            { _movementSpeedKey, _movementSpeedBuffDescription }
        };

        //_healthBuffDescription.text = $"Стоимость - {HealthBuffPrice} монет. \nИзменяет количество здоровья на {_buffImprovment.HealthBuff.Value} единиц.";
    }

    private void OnBuffPanelOpened()
    {
        _buffPanel.gameObject.SetActive(true);
    }

    private void OnBuffPanelClosed()
    {
        _buffPanel.gameObject.SetActive(false);
    }

    private void OnBuyDamageBuff()
    {
        if(_calculationFinalValue.DamageLevelImprovment != 0)
            _damageBuffCounter = _calculationFinalValue.DamageLevelImprovment;

        if (IsEnough(DamageBuffPrice) && _buffImprovment.MaxValue != _damageBuffCounter)
        {
            _buffImprovment.InitDamageLevel(_damageBuffCounter);
            _buffImprovment.UpgradeDamage();
            SpendMoney(DamageBuffPrice);
            _damageBuffCounter++;
            DamageUpgraded?.Invoke();
            _calculationFinalValue.InitLevelDamage(_damageBuffCounter);
            _calculationFinalValue.InitDamage(_buffImprovment.DamageBuff.Value);
            _dataProvider.Save();
        }
    }

    private void OnBuyHealthBuff()
    {
        if (_calculationFinalValue.HealthLevelImprovment != 0)
            _healthBuffCounter = _calculationFinalValue.HealthLevelImprovment;

        if (IsEnough(HealthBuffPrice) && _buffImprovment.MaxValue != _healthBuffCounter)
        {
            _buffImprovment.InitHealthLevel(_healthBuffCounter);
            _buffImprovment.UpgradeHealth();
            SpendMoney(HealthBuffPrice);
            _healthBuffCounter++;
            HealthUpgraded?.Invoke();
            _calculationFinalValue.InitLevelHealth(_healthBuffCounter);
            _calculationFinalValue.InitHealth(_buffImprovment.HealthBuff.Value);
            _dataProvider.Save();
        }
    }

    private void OnBuyArmorBuff()
    {
        if (_calculationFinalValue.ArmorLevelImprovment != 0)
            _armorBuffCounter = _calculationFinalValue.ArmorLevelImprovment;

        if (IsEnough(ArmorBuffPrice) && _buffImprovment.MaxValue != _armorBuffCounter)
        {
            _buffImprovment.InitAramorLevel(_armorBuffCounter);
            _buffImprovment.UpgradeArmor();
            SpendMoney(ArmorBuffPrice);
            _armorBuffCounter++;
            ArmorUpgraded?.Invoke();
            _calculationFinalValue.InitLevelArmor(_armorBuffCounter);
            _calculationFinalValue.InitArmor(_buffImprovment.ArmorBuff.Value);
            _dataProvider.Save();
        }
    }

    private void OnBuyAttackSpeedBuff()
    {
        if (_calculationFinalValue.AttackSpeedLevelImprovment != 0)
            _attackSpeedBuffCounter = _calculationFinalValue.AttackSpeedLevelImprovment;

        if (IsEnough(AttackSpeedBuffPrice) && _buffImprovment.MaxValue != _attackSpeedBuffCounter)
        {
            _buffImprovment.InitAttackSpeedLevel(_attackSpeedBuffCounter);
            _buffImprovment.UpgradeAttackSpeed();
            SpendMoney(AttackSpeedBuffPrice);
            _attackSpeedBuffCounter++;
            AttackSpeedUpgraded?.Invoke();
            _calculationFinalValue.InitLevelAttackSpeed(_attackSpeedBuffCounter);
            _calculationFinalValue.InitAttackSpeed(_buffImprovment.AttackSpeedBuff.Value);
            _dataProvider.Save();
        }
    }

    private void OnBuyMovementSpeedBuff()
    {
        if (_calculationFinalValue.MovementSpeedLevelImprovment != 0)
            _movementSpeedBuffCounter = _calculationFinalValue.MovementSpeedLevelImprovment;

        if (IsEnough(MovementSpeedBuffPrice) && _buffImprovment.MaxValue != _movementSpeedBuffCounter)
        {
            _buffImprovment.InitMovementSpeedLevel(_movementSpeedBuffCounter);
            _buffImprovment.UpgradeMovementSpeed();
            SpendMoney(MovementSpeedBuffPrice);
            _movementSpeedBuffCounter++;
            MovementSpeedUpgraded?.Invoke();
            _calculationFinalValue.InitLevelMovementSpeed(_movementSpeedBuffCounter);
            _calculationFinalValue.InitMovementSpeed(_buffImprovment.MovementSpeedBuff.Value);
            _dataProvider.Save();
        }
    }

    private void OnDamageBuffClick()
    {
        DeactivateAllViewers();
        Activate(_damageBuffPurchaseButton, _damageBuffDescription);
    }

    private void OnHealthBuffClick()
    {
        DeactivateAllViewers();
        Activate(_healthBuffPurchaseButton, _healthBuffDescription);
    }

    private void OnArmorBuffClick()
    {
        DeactivateAllViewers();
        Activate(_armorBuffPurchaseButton, _armorBuffDescription);
    }

    private void OnAttackSpeedBuffClick()
    {
        DeactivateAllViewers();
        Activate(_attackSpeedBuffPurchaseButton, _attackSpeedBuffDescription);
    }

    private void OnMovementSpeedBuffClick()
    {
        DeactivateAllViewers();
        Activate(_movementSpeedBuffPurchaseButton, _movementSpeedBuffDescription);
    }

    private void Activate(Button button, TextMeshProUGUI text)
    {
        button.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
    }

    private void Deactivate(Button button, TextMeshProUGUI text)
    {
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    private void DeactivateAllViewers()
    {
        for (int i = 0; i < _purchaseButtons.Count && i < _purchaseTexts.Count; i++)
            Deactivate(_purchaseButtons[i], _purchaseTexts[i]);
    }

    private bool IsEnough(int price) => _wallet.IsEnough(price);

    private void SpendMoney(int amount) => _wallet.Spend(amount);
}