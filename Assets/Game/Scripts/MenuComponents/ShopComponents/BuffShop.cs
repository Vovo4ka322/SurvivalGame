using System;
using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.BuffComponents;
using Game.Scripts.MenuComponents.ShopComponents.Buttons;
using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.WalletComponents;
using TMPro;

namespace Game.Scripts.MenuComponents.ShopComponents
{
    public class BuffShop : MonoBehaviour
    {
        [SerializeField] private BuffImprovement _buffImprovement;
        [SerializeField] private Image _buffPanel;
        [SerializeField] private Button _openerPanelButton;
        [SerializeField] private Button _leaverPanelButton;
        [SerializeField] private ButtonAnimation _buttonAnimation;

        [Header("Buffs buttons")]
        [SerializeField] private Button _healthBuffButton;
        [SerializeField] private Button _armorBuffButton;
        [SerializeField] private Button _damageBuffButton;
        [SerializeField] private Button _attackSpeedBuffButton;
        [SerializeField] private Button _movementSpeedBuffButton;

        [Header("Purchase buttons")]
        [SerializeField] private Button _healthBuffPurchaseButton;
        [SerializeField] private Button _armorBuffPurchaseButton;
        [SerializeField] private Button _damageBuffPurchaseButton;
        [SerializeField] private Button _attackSpeedBuffPurchaseButton;
        [SerializeField] private Button _movementSpeedBuffPurchaseButton;

        [Header("Description texts of each buff")]
        [SerializeField] private TextMeshProUGUI _healthBuffDescription;
        [SerializeField] private TextMeshProUGUI _armorBuffDescription;
        [SerializeField] private TextMeshProUGUI _damageBuffDescription;
        [SerializeField] private TextMeshProUGUI _attackSpeedBuffDescription;
        [SerializeField] private TextMeshProUGUI _movementSpeedBuffDescription;

        [Header("Price texts for each buff")]
        [SerializeField] private TextMeshProUGUI _healthBuffPriceText;
        [SerializeField] private TextMeshProUGUI _armorBuffPriceText;
        [SerializeField] private TextMeshProUGUI _damageBuffPriceText;
        [SerializeField] private TextMeshProUGUI _attackSpeedBuffPriceText;
        [SerializeField] private TextMeshProUGUI _movementSpeedBuffPriceText;

        [Header("Cost texts of each buff")]
        [SerializeField] private TextMeshProUGUI _healthBuffCost;
        [SerializeField] private TextMeshProUGUI _armorBuffCost;
        [SerializeField] private TextMeshProUGUI _damageBuffCost;
        [SerializeField] private TextMeshProUGUI _attackSpeedBuffCost;
        [SerializeField] private TextMeshProUGUI _movementSpeedBuffCost;
        
        private readonly int _startBuffPrice = 100;
        private readonly int _priceIncrement = 100;

        private Wallet _wallet;
        private IDataSaver _iDataSaver;
        private PlayerCharacteristicData _calculationFinalValue;
        private Button _currentSelectedBuffButton;

        private int _healthBuffCounter;
        private int _armorBuffCounter;
        private int _damageBuffCounter;
        private int _attackSpeedBuffCounter;
        private int _movementSpeedBuffCounter;

        public event Action HealthUpgraded;
        public event Action ArmorUpgraded;
        public event Action DamageUpgraded;
        public event Action AttackSpeedUpgraded;
        public event Action MovementSpeedUpgraded;

        public int MaxCount { get; private set; } = 5;

        private void OnEnable()
        {
            AddListener(_healthBuffButton, OnHealthBuffClick);
            AddListener(_armorBuffButton, OnArmorBuffClick);
            AddListener(_damageBuffButton, OnDamageBuffClick);
            AddListener(_attackSpeedBuffButton, OnAttackSpeedBuffClick);
            AddListener(_movementSpeedBuffButton, OnMovementSpeedBuffClick);

            AddListener(_healthBuffPurchaseButton, OnBuyHealthBuff);
            AddListener(_armorBuffPurchaseButton, OnBuyArmorBuff);
            AddListener(_damageBuffPurchaseButton, OnBuyDamageBuff);
            AddListener(_attackSpeedBuffPurchaseButton, OnBuyAttackSpeedBuff);
            AddListener(_movementSpeedBuffPurchaseButton, OnBuyMovementSpeedBuff);

            AddListener(_openerPanelButton, OnBuffPanelOpened);
            AddListener(_leaverPanelButton, OnBuffPanelClosed);
        }

        private void OnDisable()
        {
            RemoveListener(_healthBuffButton, OnHealthBuffClick);
            RemoveListener(_armorBuffButton, OnArmorBuffClick);
            RemoveListener(_damageBuffButton, OnDamageBuffClick);
            RemoveListener(_attackSpeedBuffButton, OnAttackSpeedBuffClick);
            RemoveListener(_movementSpeedBuffButton, OnMovementSpeedBuffClick);

            RemoveListener(_healthBuffPurchaseButton, OnBuyHealthBuff);
            RemoveListener(_armorBuffPurchaseButton, OnBuyArmorBuff);
            RemoveListener(_damageBuffPurchaseButton, OnBuyDamageBuff);
            RemoveListener(_attackSpeedBuffPurchaseButton, OnBuyAttackSpeedBuff);
            RemoveListener(_movementSpeedBuffPurchaseButton, OnBuyMovementSpeedBuff);

            RemoveListener(_openerPanelButton, OnBuffPanelOpened);
            RemoveListener(_leaverPanelButton, OnBuffPanelClosed);
        }

        public void Init(Wallet wallet, IDataSaver iDataSaver, IPersistentData persistentData)
        {
            _wallet = wallet;
            _iDataSaver = iDataSaver;
            _calculationFinalValue = persistentData.PlayerData.CalculationFinalValue;
        }

        private void AddListener(Button button, Action action)
        {
            button.onClick.AddListener(() => action());
        }

        private void RemoveListener(Button button, Action action)
        {
            button.onClick.RemoveListener(() => action());
        }

        private void OnBuffPanelOpened() => _buffPanel.gameObject.SetActive(true);

        private void OnBuffPanelClosed()
        {
            _buffPanel.gameObject.SetActive(false);
            _buttonAnimation.ResetSelectedBuffButton(ref _currentSelectedBuffButton);
            
            DeactivateAllViewers();
        }

        private int GetBuffPrice(int currentLevel) => _startBuffPrice + (currentLevel * _priceIncrement);

        private void UpdateBuffPriceText(TextMeshProUGUI priceText, int currentLevel) => priceText.text = $"{GetBuffPrice(currentLevel)}";

        private bool IsEnough(int price) => _wallet.IsEnough(price);

        private void SpendMoney(int amount) => _wallet.Spend(amount);

        private bool IsFull(int value) => MaxCount <= value;

        private void BuyBuff(ref int buffCounter, int currentImprovement, Button purchaseButton, TextMeshProUGUI priceText, TextMeshProUGUI costText, Action initLevel, Action upgrade, Action upgradeEvent, Action<int> updateLevelCalculation, Action<int> updateBuffCalculation)
        {
            buffCounter = currentImprovement;

            if(IsFull(buffCounter))
            {
                return;
            }

            int price = GetBuffPrice(buffCounter);

            if(!IsEnough(price))
            {
                _buttonAnimation.PlayTryPressedAnimation(purchaseButton);

                return;
            }

            _buttonAnimation.PlayPressedAnimation(purchaseButton);

            initLevel();
            upgrade();
            SpendMoney(price);
            buffCounter++;

            upgradeEvent?.Invoke();
            updateLevelCalculation(buffCounter);
            updateBuffCalculation(buffCounter);
            _iDataSaver.Save();
            UpdateBuffPriceText(priceText, buffCounter);
            Deactivate(currentImprovement, purchaseButton, costText);
        }

        private void Activate(Button button, TextMeshProUGUI description, TextMeshProUGUI cost)
        {
            button.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
            cost.gameObject.SetActive(true);
        }

        private void Deactivate(Button button, TextMeshProUGUI description, TextMeshProUGUI cost)
        {
            button.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
            cost.gameObject.SetActive(false);
        }

        private void Deactivate(int improvementLevel, Button button, TextMeshProUGUI cost)
        {
            if(improvementLevel >= MaxCount)
            {
                button.gameObject.SetActive(false);
                cost.gameObject.SetActive(false);
            }
        }

        private void DeactivateAllViewers()
        {
            Deactivate(_healthBuffPurchaseButton, _healthBuffDescription, _healthBuffCost);
            Deactivate(_armorBuffPurchaseButton, _armorBuffDescription, _armorBuffCost);
            Deactivate(_damageBuffPurchaseButton, _damageBuffDescription, _damageBuffCost);
            Deactivate(_attackSpeedBuffPurchaseButton, _attackSpeedBuffDescription, _attackSpeedBuffCost);
            Deactivate(_movementSpeedBuffPurchaseButton, _movementSpeedBuffDescription, _movementSpeedBuffCost);
        }

        private void BuffClick(Button buffButton, Button purchaseButton, TextMeshProUGUI description, TextMeshProUGUI cost, TextMeshProUGUI priceText, int currentImprovement)
        {
            _buttonAnimation.SetSelectedBuffButton(ref _currentSelectedBuffButton, buffButton);
            DeactivateAllViewers();
            Activate(purchaseButton, description, cost);
            UpdateBuffPriceText(priceText, currentImprovement);
            Deactivate(currentImprovement, purchaseButton, cost);
        }

        private void OnBuyDamageBuff()
        {
            BuyBuff(ref _damageBuffCounter, _calculationFinalValue.DamageLevelImprovment, _damageBuffPurchaseButton, _damageBuffPriceText, _damageBuffCost, () => _buffImprovement.InitDamageLevel(_damageBuffCounter), () => _buffImprovement.UpgradeDamage(), () => DamageUpgraded?.Invoke(), lvl => _calculationFinalValue.InitLevelDamage(lvl), lvl => _calculationFinalValue.InitDamage(_buffImprovement.DamageBuff.Value));
        }

        private void OnBuyHealthBuff()
        {
            BuyBuff(ref _healthBuffCounter, _calculationFinalValue.HealthLevelImprovment, _healthBuffPurchaseButton, _healthBuffPriceText, _healthBuffCost, () => _buffImprovement.InitHealthLevel(_healthBuffCounter), () => _buffImprovement.UpgradeHealth(), () => HealthUpgraded?.Invoke(), lvl => _calculationFinalValue.InitLevelHealth(lvl), lvl => _calculationFinalValue.InitHealth(_buffImprovement.HealthBuff.Value));
        }

        private void OnBuyArmorBuff()
        {
            BuyBuff(ref _armorBuffCounter, _calculationFinalValue.ArmorLevelImprovment, _armorBuffPurchaseButton, _armorBuffPriceText, _armorBuffCost, () => _buffImprovement.InitArmorLevel(_armorBuffCounter), () => _buffImprovement.UpgradeArmor(), () => ArmorUpgraded?.Invoke(), lvl => _calculationFinalValue.InitLevelArmor(lvl), lvl => _calculationFinalValue.InitArmor(_buffImprovement.ArmorBuff.Value));
        }

        private void OnBuyAttackSpeedBuff()
        {
            BuyBuff(ref _attackSpeedBuffCounter, _calculationFinalValue.AttackSpeedLevelImprovment, _attackSpeedBuffPurchaseButton, _attackSpeedBuffPriceText, _attackSpeedBuffCost, () => _buffImprovement.InitAttackSpeedLevel(_attackSpeedBuffCounter), () => _buffImprovement.UpgradeAttackSpeed(), () => AttackSpeedUpgraded?.Invoke(), lvl => _calculationFinalValue.InitLevelAttackSpeed(lvl), lvl => _calculationFinalValue.InitAttackSpeed(_buffImprovement.AttackSpeedBuff.Value));
        }

        private void OnBuyMovementSpeedBuff()
        {
            BuyBuff(ref _movementSpeedBuffCounter, _calculationFinalValue.MovementSpeedLevelImprovment, _movementSpeedBuffPurchaseButton, _movementSpeedBuffPriceText, _movementSpeedBuffCost, () => _buffImprovement.InitMovementSpeedLevel(_movementSpeedBuffCounter), () => _buffImprovement.UpgradeMovementSpeed(), () => MovementSpeedUpgraded?.Invoke(), lvl => _calculationFinalValue.InitLevelMovementSpeed(lvl), lvl => _calculationFinalValue.InitMovementSpeed(_buffImprovement.MovementSpeedBuff.Value));
        }

        private void OnDamageBuffClick() => BuffClick(_damageBuffButton, _damageBuffPurchaseButton, _damageBuffDescription, _damageBuffCost, _damageBuffPriceText, _calculationFinalValue.DamageLevelImprovment);

        private void OnHealthBuffClick() => BuffClick(_healthBuffButton, _healthBuffPurchaseButton, _healthBuffDescription, _healthBuffCost, _healthBuffPriceText, _calculationFinalValue.HealthLevelImprovment);

        private void OnArmorBuffClick() => BuffClick(_armorBuffButton, _armorBuffPurchaseButton, _armorBuffDescription, _armorBuffCost, _armorBuffPriceText, _calculationFinalValue.ArmorLevelImprovment);

        private void OnAttackSpeedBuffClick() => BuffClick(_attackSpeedBuffButton, _attackSpeedBuffPurchaseButton, _attackSpeedBuffDescription, _attackSpeedBuffCost, _attackSpeedBuffPriceText, _calculationFinalValue.AttackSpeedLevelImprovment);

        private void OnMovementSpeedBuffClick() => BuffClick(_movementSpeedBuffButton, _movementSpeedBuffPurchaseButton, _movementSpeedBuffDescription, _movementSpeedBuffCost, _movementSpeedBuffPriceText, _calculationFinalValue.MovementSpeedLevelImprovment);
    }
}
