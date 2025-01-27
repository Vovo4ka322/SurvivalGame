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

    private Wallet _wallet;
    private IDataProvider _dataProvider;
    private int _sumForNextPrice = 200;

    private int _healthBuffCounter = 0;
    private int _armorBuffCounter = 0;
    private int _damageBuffCounter = 0;
    private int _attackSpeedBuffCounter = 0;
    private int _movementSpeedBuffCounter = 0;

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

    [field: SerializeField] public int HealthBuffPrice { get; private set; }

    [field: SerializeField] public int ArmorBuffPrice { get; private set; }

    [field: SerializeField] public int DamageBuffPrice { get; private set; }

    [field: SerializeField] public int AttackSpeedBuffPrice { get; private set; }

    [field: SerializeField] public int MovementSpeedBuffPrice { get; private set; }

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
    }

    public void Init(Wallet wallet, IDataProvider dataProvider)
    {
        _wallet = wallet;
        _dataProvider = dataProvider;

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
    }

    private void OnBuffPanelOpened()
    {
        _buffPanel.gameObject.SetActive(true);
    }

    private void OnBuyDamageBuff()
    {
        if (IsEnough(DamageBuffPrice) && _buffImprovment.MaxValue != _damageBuffCounter)
        {
            _buffImprovment.UpgradeDamage();
            SpendMoney(DamageBuffPrice);
            DamageBuffPrice += _sumForNextPrice;
            _damageBuffCounter++;
            DamageUpgraded?.Invoke();
            //_dataProvider.Save();
        }
    }

    private void OnBuyHealthBuff()
    {
        if (IsEnough(HealthBuffPrice) && _buffImprovment.MaxValue != _healthBuffCounter)
        {
            _buffImprovment.UpgradeHealth();
            SpendMoney(HealthBuffPrice);
            HealthBuffPrice += _sumForNextPrice;
            _healthBuffCounter++;
            HealthUpgraded?.Invoke();
            //_dataProvider.Save();
        }
    }

    private void OnBuyArmorBuff()
    {
        if (IsEnough(ArmorBuffPrice) && _buffImprovment.MaxValue != _armorBuffCounter)
        {
            _buffImprovment.UpgradeArmor();
            SpendMoney(ArmorBuffPrice);
            ArmorBuffPrice += _sumForNextPrice;
            _armorBuffCounter++;
            ArmorUpgraded?.Invoke();
            //_dataProvider.Save();
        }
    }

    private void OnBuyAttackSpeedBuff()
    {
        if (IsEnough(AttackSpeedBuffPrice) && _buffImprovment.MaxValue != _attackSpeedBuffCounter)
        {
            _buffImprovment.UpgradeAttackSpeed();
            SpendMoney(AttackSpeedBuffPrice);
            AttackSpeedBuffPrice += _sumForNextPrice;
            _attackSpeedBuffCounter++;
            AttackSpeedUpgraded?.Invoke();
            //_dataProvider.Save();
        }
    }

    private void OnBuyMovementSpeedBuff()
    {
        if (IsEnough(MovementSpeedBuffPrice) && _buffImprovment.MaxValue != _movementSpeedBuffCounter)
        {
            _buffImprovment.UpgradeMovementSpeed();
            SpendMoney(MovementSpeedBuffPrice);
            MovementSpeedBuffPrice += _sumForNextPrice;
            _movementSpeedBuffCounter++;
            MovementSpeedUpgraded?.Invoke();
            //_dataProvider.Save();
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