using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.BuffComponents;
using Game.Scripts.MenuComponents.ShopComponents.Buttons;
using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.WalletComponents;
using TMPro;

namespace Game.Scripts.MenuComponents.ShopComponents.CharacteristicsComponents
{
    public class BuffShop : MonoBehaviour
    {
        [SerializeField] private BuffImprovement _buffImprovement;
        [SerializeField] private BuffImprovementViewer _buffImprovementViewer;
        [SerializeField] private Image _buffPanel;
        [SerializeField] private Button _openerPanelButton;
        [SerializeField] private Button _leaverPanelButton;
        [SerializeField] private Button _resetBuffButton;
        [SerializeField] private ButtonAnimation _buttonAnimation;
        [SerializeField] private ParticleSystem _buffParticle;
        
        [Header("UI Configurations")]
        [SerializeField] private List<BuffUIConfig> _buffUIConfigs;

        private readonly Dictionary<BuffType, int> _buffCounters = new Dictionary<BuffType, int>
        {
            { BuffType.Health, 0 },
            { BuffType.Armor, 0 },
            { BuffType.Damage, 0 },
            { BuffType.AttackSpeed, 0 },
            { BuffType.MovementSpeed, 0 }
        };
        private readonly int _startBuffPrice = 100;

        private Wallet _wallet;
        private IDataSaver _iDataSaver;
        private PlayerCharacteristicData _calculationFinalValue;
        private Button _currentSelectedBuffButton;

        public event Action HealthUpgraded;
        public event Action ArmorUpgraded;
        public event Action DamageUpgraded;
        public event Action AttackSpeedUpgraded;
        public event Action MovementSpeedUpgraded;

        public int MaxCount { get; private set; } = 5;

        private void Awake()
        {
            if(_buffParticle != null && !_buffParticle.gameObject.scene.isLoaded)
            {
                _buffParticle = Instantiate(_buffParticle);
            }
        }

        private void OnEnable()
        {
            foreach(BuffUIConfig config in _buffUIConfigs)
            {
                config.BuffButton.onClick.AddListener(() => OnBuffButtonClick(config));
                config.PurchaseButton.onClick.AddListener(() => OnBuyBuff(config.BuffType));
            }
            
            _openerPanelButton.onClick.AddListener(OnBuffPanelOpened);
            _leaverPanelButton.onClick.AddListener(OnBuffPanelClosed);
            _resetBuffButton.onClick.AddListener(OnResetBuffs);
        }

        private void OnDisable()
        {
            foreach(BuffUIConfig config in _buffUIConfigs)
            {
                config.BuffButton.onClick.RemoveAllListeners();
                config.PurchaseButton.onClick.RemoveAllListeners();
            }
            
            _openerPanelButton.onClick.RemoveListener(OnBuffPanelOpened);
            _leaverPanelButton.onClick.RemoveListener(OnBuffPanelClosed);
            _resetBuffButton.onClick.RemoveListener(OnResetBuffs);
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
            _currentSelectedBuffButton = null;
            DeactivateAllViewers();
            ResetParticle();
        }

        private void ResetParticle()
        {
            if(_buffParticle != null)
            {
                _buffParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _buffParticle.gameObject.SetActive(false);
                _buffParticle.transform.SetParent(transform, false);
            }
        }

        private void ActivateParticle(Button targetButton)
        {
            if(_buffParticle != null)
            {
                if(!_buffParticle.gameObject.activeSelf)
                {
                    _buffParticle.gameObject.SetActive(true);
                }

                _buffParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _buffParticle.transform.SetParent(targetButton.transform, false);
                _buffParticle.transform.localPosition = Vector3.zero;
                _buffParticle.Play();
            }
        }

        private void OnBuffButtonClick(BuffUIConfig config)
        {
            SetSelectedBuffButton(config.BuffButton);
            DeactivateAllViewers();
            Activate(config.PurchaseButton, config.Description, config.CostText);
            UpdatePriceText(config.PriceText, GetCurrentBuffLevel(config.BuffType));
            DeactivateIfMax(GetLevelFromCalc(config.BuffType), config.PurchaseButton, config.CostText);
        }

        private void SetSelectedBuffButton(Button newButton)
        {
            if(newButton == null || _currentSelectedBuffButton == newButton)
            {
                return;
            }

            ActivateParticle(newButton);
            _currentSelectedBuffButton = newButton;
        }

        private void Activate(Button button, TextMeshProUGUI description, TextMeshProUGUI costText)
        {
            button.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
            costText.gameObject.SetActive(true);
        }

        private void Deactivate(Button button, TextMeshProUGUI description, TextMeshProUGUI costText)
        {
            button.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
            costText.gameObject.SetActive(false);
        }

        private void DeactivateIfMax(int currentLevel, Button purchaseButton, TextMeshProUGUI costText)
        {
            if(currentLevel >= MaxCount)
            {
                purchaseButton.gameObject.SetActive(false);
                costText.gameObject.SetActive(false);
            }
        }

        private void DeactivateAllViewers()
        {
            foreach(BuffUIConfig config in _buffUIConfigs)
            {
                Deactivate(config.PurchaseButton, config.Description, config.CostText);
            }
        }

        private void UpdatePriceText(TextMeshProUGUI priceText, int currentLevel)
        {
            priceText.text = $"{GetBuffPrice(currentLevel)}";
        }

        private void SpendMoney(int amount) => _wallet.Spend(amount);

        private void OnBuyBuff(BuffType buffType)
        {
            int counter = _buffCounters[buffType];
            int levelFromCalc = GetLevelFromCalc(buffType);

            if(IsFull(counter))
            {
                return;
            }

            int price = GetBuffPrice(counter);
            BuffUIConfig config = _buffUIConfigs.First(c => c.BuffType == buffType);

            if(!IsEnough(price))
            {
                _buttonAnimation.PlayTryPressedAnimation(config.PurchaseButton);

                return;
            }

            _buttonAnimation.PlayPressedAnimation(config.PurchaseButton);
            
            InitBuffLevel(buffType, counter);
            UpgradeBuff(buffType);
            SpendMoney(price);
            _buffCounters[buffType] = counter + 1;
            InvokeBuffUpgradedEvent(buffType);
            UpdateCalcLevel(buffType, _buffCounters[buffType]);
            UpdateCalcBuffValue(buffType, GetBuffValue(buffType));
            _iDataSaver.Save();
            UpdatePriceText(config.PriceText, _buffCounters[buffType]);
            DeactivateIfMax(GetLevelFromCalc(buffType), config.PurchaseButton, config.CostText);
        }
        
        private void InvokeBuffUpgradedEvent(BuffType buffType)
        {
            switch(buffType)
            {
                case BuffType.Health:
                    HealthUpgraded?.Invoke();

                    break;
                case BuffType.Armor:
                    ArmorUpgraded?.Invoke();

                    break;
                case BuffType.Damage:
                    DamageUpgraded?.Invoke();

                    break;
                case BuffType.AttackSpeed:
                    AttackSpeedUpgraded?.Invoke();

                    break;
                case BuffType.MovementSpeed:
                    MovementSpeedUpgraded?.Invoke();

                    break;
            }
        }
        
        private void UpdateCalcLevel(BuffType buffType, int newLevel)
        {
            switch(buffType)
            {
                case BuffType.Health:
                    _calculationFinalValue.InitLevelHealth(newLevel);

                    break;
                case BuffType.Armor:
                    _calculationFinalValue.InitLevelArmor(newLevel);

                    break;
                case BuffType.Damage:
                    _calculationFinalValue.InitLevelDamage(newLevel);

                    break;
                case BuffType.AttackSpeed:
                    _calculationFinalValue.InitLevelAttackSpeed(newLevel);

                    break;
                case BuffType.MovementSpeed:
                    _calculationFinalValue.InitLevelMovementSpeed(newLevel);

                    break;
            }
        }
        
        private void UpdateCalcBuffValue(BuffType buffType, float buffValue)
        {
            switch(buffType)
            {
                case BuffType.Health:
                    _calculationFinalValue.InitHealth(buffValue);

                    break;
                case BuffType.Armor:
                    _calculationFinalValue.InitArmor(buffValue);

                    break;
                case BuffType.Damage:
                    _calculationFinalValue.InitDamage(buffValue);

                    break;
                case BuffType.AttackSpeed:
                    _calculationFinalValue.InitAttackSpeed(buffValue);

                    break;
                case BuffType.MovementSpeed:
                    _calculationFinalValue.InitMovementSpeed(buffValue);

                    break;
            }
        }
        
        private void InitBuffLevel(BuffType buffType, int currentLevel)
        {
            switch(buffType)
            {
                case BuffType.Health:
                    _buffImprovement.InitHealthLevel(currentLevel);

                    break;
                case BuffType.Armor:
                    _buffImprovement.InitArmorLevel(currentLevel);

                    break;
                case BuffType.Damage:
                    _buffImprovement.InitDamageLevel(currentLevel);

                    break;
                case BuffType.AttackSpeed:
                    _buffImprovement.InitAttackSpeedLevel(currentLevel);

                    break;
                case BuffType.MovementSpeed:
                    _buffImprovement.InitMovementSpeedLevel(currentLevel);

                    break;
            }
        }
        
        private void UpgradeBuff(BuffType buffType)
        {
            switch(buffType)
            {
                case BuffType.Health:
                    _buffImprovement.UpgradeHealth();

                    break;
                case BuffType.Armor:
                    _buffImprovement.UpgradeArmor();

                    break;
                case BuffType.Damage:
                    _buffImprovement.UpgradeDamage();

                    break;
                case BuffType.AttackSpeed:
                    _buffImprovement.UpgradeAttackSpeed();

                    break;
                case BuffType.MovementSpeed:
                    _buffImprovement.UpgradeMovementSpeed();

                    break;
            }
        }
        
        private void OnResetBuffs()
        {
            foreach (BuffType key in _buffCounters.Keys.ToList())
            {
                _buffCounters[key] = 0;
            }
            
            UpdateCalcLevel(BuffType.Health, 0);
            UpdateCalcLevel(BuffType.Armor, 0);
            UpdateCalcLevel(BuffType.Damage, 0);
            UpdateCalcLevel(BuffType.AttackSpeed, 0);
            UpdateCalcLevel(BuffType.MovementSpeed, 0);
        
            UpdateCalcBuffValue(BuffType.Health, 0f);
            UpdateCalcBuffValue(BuffType.Armor, 0f);
            UpdateCalcBuffValue(BuffType.Damage, 0f);
            UpdateCalcBuffValue(BuffType.AttackSpeed, 0f);
            UpdateCalcBuffValue(BuffType.MovementSpeed, 0f);
            
            foreach (BuffUIConfig config in _buffUIConfigs)
            {
                UpdatePriceText(config.PriceText, 0);
            }
            
            _iDataSaver.Save();
            _currentSelectedBuffButton = null;
            DeactivateAllViewers();
            _buffImprovementViewer.ResetUpgrades();
        }
        
        private float GetBuffValue(BuffType buffType)
        {
            return buffType switch
            {
                BuffType.Health => _buffImprovement.HealthBuff.Value,
                BuffType.Armor => _buffImprovement.ArmorBuff.Value,
                BuffType.Damage => _buffImprovement.DamageBuff.Value,
                BuffType.AttackSpeed => _buffImprovement.AttackSpeedBuff.Value,
                BuffType.MovementSpeed => _buffImprovement.MovementSpeedBuff.Value,
                _ => 0f,
            };
        }
        
        private int GetBuffPrice(int currentLevel) => (currentLevel + 1) * _startBuffPrice;
        
        private int GetCurrentBuffLevel(BuffType buffType) => _buffCounters[buffType];
        
        private int GetLevelFromCalc(BuffType buffType)
        {
            return buffType switch
            {
                BuffType.Health => _calculationFinalValue.HealthLevelImprovment,
                BuffType.Armor => _calculationFinalValue.ArmorLevelImprovment,
                BuffType.Damage => _calculationFinalValue.DamageLevelImprovment,
                BuffType.AttackSpeed => _calculationFinalValue.AttackSpeedLevelImprovment,
                BuffType.MovementSpeed => _calculationFinalValue.MovementSpeedLevelImprovment,
                _ => 0,
            };
        }
        
        private bool IsEnough(int price) => _wallet.IsEnough(price);
        
        private bool IsFull(int value) => value >= MaxCount;
    }
}
