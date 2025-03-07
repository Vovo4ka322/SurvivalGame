using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MenuComponents.ShopComponents.Viewers
{
    public class BuffShopViewer : MonoBehaviour
    {
        [Header("Buffs buttons")]
        [SerializeField] private Button _healthBuffButton;
        [SerializeField] private Button _armorBuffButton;
        [SerializeField] private Button _damageBuffButton;
        [SerializeField] private Button _attackSpeedBuffButton;
        [SerializeField] private Button _movementSpeedBuffButton;

        [Header("Text and button of purchase of every buffs buttons")]
        [SerializeField] private Button _healthBuffPurchaseButton;
        [SerializeField] private Text _healthBuffDescription;
        [SerializeField] private Button _armorBuffPurchaseButton;
        [SerializeField] private Text _armorBuffDescription;
        [SerializeField] private Button _damageBuffPurchaseButton;
        [SerializeField] private Text _damageBuffDescription;
        [SerializeField] private Button _attackSpeedBuffPurchaseButton;
        [SerializeField] private Text _attackSpeedBuffDescription;
        [SerializeField] private Button _movementSpeedBuffPurchaseButton;
        [SerializeField] private Text _movementSpeedBuffDescription;
        
        private readonly int _healthKey = 0;
        private readonly int _armorKey = 1;
        private readonly int _damageKey = 2;
        private readonly int _attackSpeedKey = 3;
        private readonly int _movementSpeedKey = 4;
        
        private Dictionary<int, Button> _purchaseButtons;
        private Dictionary<int, Text> _purchaseTexts;
        
        private BuffImprovment _buffImprovment;
        private BuffShop _buffShop;
        
        private void OnEnable()
        {
            _healthBuffButton.onClick.AddListener(OnHealthBuffClick);
            _armorBuffButton.onClick.AddListener(OnArmorBuffClick);
            _damageBuffButton.onClick.AddListener(OnDamageBuffClick);
            _attackSpeedBuffButton.onClick.AddListener(OnAttackSpeedBuffClick);
            _movementSpeedBuffButton.onClick.AddListener(OnMovementSpeedBuffClick);
            
            //_damageBuffPurchaseButton.onClick.AddListener(OnBuyDamageBuff);
            //_healthBuffPurchaseButton.onClick.AddListener(OnBuyHealthBuff);
            //_armorBuffPurchaseButton.onClick.AddListener(OnBuyArmorBuff);
            //_attackSpeedBuffPurchaseButton.onClick.AddListener(OnBuyAttackSpeedBuff);
            //_movementSpeedBuffPurchaseButton.onClick.AddListener(OnBuyMovementSpeedBuff);
        }
        
        public void Init(BuffImprovment buffImprovment, BuffShop buffShop)
        {
            _buffShop = buffShop;
            _buffImprovment = buffImprovment;
            
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
        
        private void Activate(Button button, Text text)
        {
            button.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }
        
        private void Deactivate(Button button, Text text)
        {
            button.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
        
        private void DeactivateAllViewers()
        {
            for(int i = 0; i < _purchaseButtons.Count && i < _purchaseTexts.Count; i++)
            {
                Deactivate(_purchaseButtons[i], _purchaseTexts[i]);
            }
        }
    }
}