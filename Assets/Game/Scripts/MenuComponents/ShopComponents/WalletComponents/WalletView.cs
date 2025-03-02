using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MenuComponents.ShopComponents.WalletComponents
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private Text _value;

        private Wallet _wallet;

        private void OnEnable()
        {
            if (_wallet != null)
                _wallet.CoinsChanged += UpdateValue;
        }

        private void OnDisable()
        {
            if (_wallet != null)
                _wallet.CoinsChanged -= UpdateValue;
        }

        public void Initialize(Wallet wallet)
        {
            _wallet = wallet;

            UpdateValue(_wallet.GetCurrentCoins());
        }

        private void UpdateValue(int value) => _value.text = value.ToString();
    }
}