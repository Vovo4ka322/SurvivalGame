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
        
        private readonly int _startBuffPrice = 50;
        
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
        
        public void Init(Wallet wallet, IDataSaver iDataSaver, IPersistentData persistentData)
        {
            _wallet = wallet;
            _iDataSaver = iDataSaver;
            _calculationFinalValue = persistentData.PlayerData.CalculationFinalValue;
        }
        
        private void OnBuffPanelOpened()
        {
            _buffPanel.gameObject.SetActive(true);
        }
        
        private void OnBuffPanelClosed()
        {
            _buffPanel.gameObject.SetActive(false);
        }
        
        private int GetBuffPrice(int currentLevel) => (currentLevel + 1) * _startBuffPrice;
        
        private void UpdatePriceText(TextMeshProUGUI priceText, int level)
        {
            priceText.text = GetBuffPrice(level).ToString();
        }
        
        private void PurchaseBuff(ref int buffCounter, int persistentLevel, Button purchaseButton, Action<int> initLevel, Action upgradeBuff, Action onUpgraded, Action<int> updateCalculationLevel, Action updateCalculationValue, Action<int> updatePriceText)
        {
            if (persistentLevel != 0)
            {
                buffCounter = persistentLevel;
            }

            int price = GetBuffPrice(buffCounter);

            if (!IsEnough(price))
            {
                _buttonAnimation.PlayTryPressedAnimation(purchaseButton);
                return;
            }

            _buttonAnimation.PlayPressedAnimation(purchaseButton);
            initLevel(buffCounter);
            upgradeBuff();
            SpendMoney(price);
            buffCounter++;
            onUpgraded?.Invoke();
            updateCalculationLevel(buffCounter);
            updateCalculationValue();
            _iDataSaver.Save();
            updatePriceText(buffCounter);
        }
        
        private void OnBuyDamageBuff()
        {
            PurchaseBuff(ref _damageBuffCounter, _calculationFinalValue.DamageLevelImprovment, _damageBuffPurchaseButton, level => _buffImprovement.InitDamageLevel(level), () => _buffImprovement.UpgradeDamage(), () => DamageUpgraded?.Invoke(),
                level => _calculationFinalValue.InitLevelDamage(level), () => _calculationFinalValue.InitDamage(_buffImprovement.DamageBuff.Value), level => UpdatePriceText(_damageBuffPriceText, level));
        }
        
        private void OnBuyHealthBuff()
        {
            PurchaseBuff(ref _healthBuffCounter, _calculationFinalValue.HealthLevelImprovment, _healthBuffPurchaseButton, level => _buffImprovement.InitHealthLevel(level), () => _buffImprovement.UpgradeHealth(), () => HealthUpgraded?.Invoke(),
                level => _calculationFinalValue.InitLevelHealth(level), () => _calculationFinalValue.InitHealth(_buffImprovement.HealthBuff.Value), level => UpdatePriceText(_healthBuffPriceText, level));
        }
        
        private void OnBuyArmorBuff()
        {
            PurchaseBuff(ref _armorBuffCounter, _calculationFinalValue.ArmorLevelImprovment, _armorBuffPurchaseButton, level => _buffImprovement.InitArmorLevel(level), () => _buffImprovement.UpgradeArmor(), () => ArmorUpgraded?.Invoke(),
                level => _calculationFinalValue.InitLevelArmor(level), () => _calculationFinalValue.InitArmor(_buffImprovement.ArmorBuff.Value), level => UpdatePriceText(_armorBuffPriceText, level));
        }
        
        private void OnBuyAttackSpeedBuff()
        {
            PurchaseBuff(ref _attackSpeedBuffCounter, _calculationFinalValue.AttackSpeedLevelImprovment, _attackSpeedBuffPurchaseButton, level => _buffImprovement.InitAttackSpeedLevel(level), () => _buffImprovement.UpgradeAttackSpeed(), () => AttackSpeedUpgraded?.Invoke(),
                level => _calculationFinalValue.InitLevelAttackSpeed(level), () => _calculationFinalValue.InitAttackSpeed(_buffImprovement.AttackSpeedBuff.Value), level => UpdatePriceText(_attackSpeedBuffPriceText, level));
        }
        
        private void OnBuyMovementSpeedBuff()
        {
            PurchaseBuff(ref _movementSpeedBuffCounter, _calculationFinalValue.MovementSpeedLevelImprovment, _movementSpeedBuffPurchaseButton, level => _buffImprovement.InitMovementSpeedLevel(level), () => _buffImprovement.UpgradeMovementSpeed(), () => MovementSpeedUpgraded?.Invoke(),
                level => _calculationFinalValue.InitLevelMovementSpeed(level), () => _calculationFinalValue.InitMovementSpeed(_buffImprovement.MovementSpeedBuff.Value), level => UpdatePriceText(_movementSpeedBuffPriceText, level));
        }
        
        private void OnBuffClick(Button buffButton, Button purchaseButton, TextMeshProUGUI descriptionText, Action<int> updatePriceText, int persistentLevel)
        {
            _buttonAnimation.SetSelectedBuffButton(ref _currentSelectedBuffButton, buffButton);
            DeactivateAllViewers();
            Activate(purchaseButton, descriptionText);
            updatePriceText(persistentLevel);
        }
        
        private void OnDamageBuffClick()
        {
            OnBuffClick(_damageBuffButton, _damageBuffPurchaseButton, _damageBuffDescription, level => UpdatePriceText(_damageBuffPriceText, level), _calculationFinalValue.DamageLevelImprovment);
        }
        
        private void OnHealthBuffClick()
        {
            OnBuffClick(_healthBuffButton, _healthBuffPurchaseButton, _healthBuffDescription, level => UpdatePriceText(_healthBuffPriceText, level), _calculationFinalValue.HealthLevelImprovment);
        }
        
        private void OnArmorBuffClick()
        {
            OnBuffClick(_armorBuffButton, _armorBuffPurchaseButton, _armorBuffDescription, level => UpdatePriceText(_armorBuffPriceText, level), _calculationFinalValue.ArmorLevelImprovment);
        }
        
        private void OnAttackSpeedBuffClick()
        {
            OnBuffClick(_attackSpeedBuffButton, _attackSpeedBuffPurchaseButton, _attackSpeedBuffDescription, level => UpdatePriceText(_attackSpeedBuffPriceText, level), _calculationFinalValue.AttackSpeedLevelImprovment);
        }
        
        private void OnMovementSpeedBuffClick()
        {
            OnBuffClick(_movementSpeedBuffButton, _movementSpeedBuffPurchaseButton, _movementSpeedBuffDescription, level => UpdatePriceText(_movementSpeedBuffPriceText, level), _calculationFinalValue.MovementSpeedLevelImprovment);
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
            Deactivate(_healthBuffPurchaseButton, _healthBuffDescription);
            Deactivate(_armorBuffPurchaseButton, _armorBuffDescription);
            Deactivate(_damageBuffPurchaseButton, _damageBuffDescription);
            Deactivate(_attackSpeedBuffPurchaseButton, _attackSpeedBuffDescription);
            Deactivate(_movementSpeedBuffPurchaseButton, _movementSpeedBuffDescription);
        }
        
        private bool IsEnough(int price) => _wallet.IsEnough(price);
        
        private void SpendMoney(int amount) => _wallet.Spend(amount);
    }
}