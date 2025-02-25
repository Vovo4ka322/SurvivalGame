using TMPro;
using UnityEngine;

namespace MenuComponents.ShopComponents.WalletComponents
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        
        private Wallet _wallet;
        
        private void OnDestroy() => _wallet.CoinsChanged -= UpdateValue;
        
        public void Initialize(Wallet wallet)
        {
            _wallet = wallet;
            
            UpdateValue(_wallet.GetCurrentCoins());
            
            _wallet.CoinsChanged += UpdateValue;
        }
        
        private void UpdateValue(int value) => _value.text = value.ToString();
    }
}