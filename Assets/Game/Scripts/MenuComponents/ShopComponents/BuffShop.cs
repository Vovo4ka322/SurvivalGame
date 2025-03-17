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
        [SerializeField] private ParticleSystem _buffParticle;
        
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

        private void Awake()
        {
            if (_buffParticle != null && ! _buffParticle.gameObject.scene.isLoaded)
            {
                _buffParticle = Instantiate(_buffParticle);
            }
        }

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
        
        private void UpdateHealthBuffPriceText(int currentLevel)
        {
            _healthBuffPriceText.text = $"{GetBuffPrice(currentLevel)}";
        }
    
        private void UpdateArmorBuffPriceText(int currentLevel)
        {
            _armorBuffPriceText.text = $"{GetBuffPrice(currentLevel)}";
        }
    
        private void UpdateDamageBuffPriceText(int currentLevel)
        {
            _damageBuffPriceText.text = $"{GetBuffPrice(currentLevel)}";
        }
    
        private void UpdateAttackSpeedBuffPriceText(int currentLevel)
        {
            _attackSpeedBuffPriceText.text = $"{GetBuffPrice(currentLevel)}";
        }
    
        private void UpdateMovementSpeedBuffPriceText(int currentLevel)
        {
            _movementSpeedBuffPriceText.text = $"{GetBuffPrice(currentLevel)}";
        }
        
        private void OnBuyDamageBuff()
        {
            if(_calculationFinalValue.DamageLevelImprovment != 0)
            {
                _damageBuffCounter = _calculationFinalValue.DamageLevelImprovment;
            }
            
            int price = GetBuffPrice(_damageBuffCounter);
            
            if(!IsEnough(price))
            {
                _buttonAnimation.PlayTryPressedAnimation(_damageBuffPurchaseButton);
                return;
            }
            
            _buttonAnimation.PlayPressedAnimation(_damageBuffPurchaseButton);
            
            _buffImprovement.InitDamageLevel(_damageBuffCounter);
            _buffImprovement.UpgradeDamage();
            SpendMoney(price);
            _damageBuffCounter++;
            DamageUpgraded?.Invoke();
            _calculationFinalValue.InitLevelDamage(_damageBuffCounter);
            _calculationFinalValue.InitDamage(_buffImprovement.DamageBuff.Value);
            _iDataSaver.Save();
            UpdateDamageBuffPriceText(_damageBuffCounter);
        }
        
        private void OnBuyHealthBuff()
        {
            if(_calculationFinalValue.HealthLevelImprovment != 0)
            {
                _healthBuffCounter = _calculationFinalValue.HealthLevelImprovment;
            }
            
            int price = GetBuffPrice(_healthBuffCounter);
            
            if(!IsEnough(price))
            {
                _buttonAnimation.PlayTryPressedAnimation(_healthBuffPurchaseButton);
                return;
            }
            
            _buttonAnimation.PlayPressedAnimation(_healthBuffPurchaseButton);
            
            _buffImprovement.InitHealthLevel(_healthBuffCounter);
            _buffImprovement.UpgradeHealth();
            SpendMoney(price);
            _healthBuffCounter++;
            HealthUpgraded?.Invoke();
            _calculationFinalValue.InitLevelHealth(_healthBuffCounter);
            _calculationFinalValue.InitHealth(_buffImprovement.HealthBuff.Value);
            _iDataSaver.Save();
            UpdateHealthBuffPriceText(_healthBuffCounter);
        }
        
        private void OnBuyArmorBuff()
        {
            if(_calculationFinalValue.ArmorLevelImprovment != 0)
            {
                _armorBuffCounter = _calculationFinalValue.ArmorLevelImprovment;
            }
            
            int price = GetBuffPrice(_armorBuffCounter);
            
            if(!IsEnough(price))
            {
                _buttonAnimation.PlayTryPressedAnimation(_armorBuffPurchaseButton);
                return;
            }
        
            _buttonAnimation.PlayPressedAnimation(_armorBuffPurchaseButton);
            
            _buffImprovement.InitArmorLevel(_armorBuffCounter);
            _buffImprovement.UpgradeArmor();
            SpendMoney(price);
            _armorBuffCounter++;
            ArmorUpgraded?.Invoke();
            _calculationFinalValue.InitLevelArmor(_armorBuffCounter);
            _calculationFinalValue.InitArmor(_buffImprovement.ArmorBuff.Value);
            _iDataSaver.Save();
            UpdateArmorBuffPriceText(_armorBuffCounter);
        }
        
        private void OnBuyAttackSpeedBuff()
        {
            if(_calculationFinalValue.AttackSpeedLevelImprovment != 0)
            {
                _attackSpeedBuffCounter = _calculationFinalValue.AttackSpeedLevelImprovment;
            }
            
            int price = GetBuffPrice(_attackSpeedBuffCounter);
            
            if(!IsEnough(price))
            {
                _buttonAnimation.PlayTryPressedAnimation(_attackSpeedBuffPurchaseButton);
                return;
            }
        
            _buttonAnimation.PlayPressedAnimation(_attackSpeedBuffPurchaseButton);
            
            _buffImprovement.InitAttackSpeedLevel(_attackSpeedBuffCounter);
            _buffImprovement.UpgradeAttackSpeed();
            SpendMoney(price);
            _attackSpeedBuffCounter++;
            AttackSpeedUpgraded?.Invoke();
            _calculationFinalValue.InitLevelAttackSpeed(_attackSpeedBuffCounter);
            _calculationFinalValue.InitAttackSpeed(_buffImprovement.AttackSpeedBuff.Value);
            _iDataSaver.Save();
            UpdateAttackSpeedBuffPriceText(_attackSpeedBuffCounter);
        }
        
        private void OnBuyMovementSpeedBuff()
        {
            if(_calculationFinalValue.MovementSpeedLevelImprovment != 0)
            {
                _movementSpeedBuffCounter = _calculationFinalValue.MovementSpeedLevelImprovment;
            }
            
            int price = GetBuffPrice(_movementSpeedBuffCounter);
            
            if(!IsEnough(price))
            {
                _buttonAnimation.PlayTryPressedAnimation(_movementSpeedBuffPurchaseButton);
                return;
            }
        
            _buttonAnimation.PlayPressedAnimation(_movementSpeedBuffPurchaseButton);
            
            _buffImprovement.InitMovementSpeedLevel(_movementSpeedBuffCounter);
            _buffImprovement.UpgradeMovementSpeed();
            SpendMoney(price);
            _movementSpeedBuffCounter++;
            MovementSpeedUpgraded?.Invoke();
            _calculationFinalValue.InitLevelMovementSpeed(_movementSpeedBuffCounter);
            _calculationFinalValue.InitMovementSpeed(_buffImprovement.MovementSpeedBuff.Value);
            _iDataSaver.Save();
            UpdateMovementSpeedBuffPriceText(_movementSpeedBuffCounter);
        }
        
        private void OnDamageBuffClick()
        {
            SetSelectedBuffButton(_damageBuffButton);
            DeactivateAllViewers();
            Activate(_damageBuffPurchaseButton, _damageBuffDescription);
            UpdateDamageBuffPriceText(_calculationFinalValue.DamageLevelImprovment);
        }
        
        private void OnHealthBuffClick()
        {
            SetSelectedBuffButton(_healthBuffButton);
            DeactivateAllViewers();
            Activate(_healthBuffPurchaseButton, _healthBuffDescription);
            UpdateHealthBuffPriceText(_calculationFinalValue.HealthLevelImprovment);
        }
        
        private void OnArmorBuffClick()
        {
            SetSelectedBuffButton(_armorBuffButton);
            DeactivateAllViewers();
            Activate(_armorBuffPurchaseButton, _armorBuffDescription);
            UpdateArmorBuffPriceText(_calculationFinalValue.ArmorLevelImprovment);
        }
        
        private void OnAttackSpeedBuffClick()
        {
            SetSelectedBuffButton(_attackSpeedBuffButton);
            DeactivateAllViewers();
            Activate(_attackSpeedBuffPurchaseButton, _attackSpeedBuffDescription);
            UpdateAttackSpeedBuffPriceText(_calculationFinalValue.AttackSpeedLevelImprovment);
        }
        
        private void OnMovementSpeedBuffClick()
        {
            SetSelectedBuffButton(_movementSpeedBuffButton);
            DeactivateAllViewers();
            Activate(_movementSpeedBuffPurchaseButton, _movementSpeedBuffDescription);
            UpdateMovementSpeedBuffPriceText(_calculationFinalValue.MovementSpeedLevelImprovment);
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

        private void SetSelectedBuffButton(Button newButton)
        {
            if(newButton == null)
            {
                return;
            }

            if(_currentSelectedBuffButton == newButton)
            {
                return;
            }

            if(_buffParticle != null)
            {
                _buffParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _buffParticle.transform.SetParent(newButton.transform, false);
                _buffParticle.transform.localPosition = Vector3.zero;
                _buffParticle.Play();
            }

            _currentSelectedBuffButton = newButton;
        }
    }
}