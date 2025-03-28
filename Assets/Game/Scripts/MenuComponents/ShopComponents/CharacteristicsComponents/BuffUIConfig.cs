using System;
using UnityEngine.UI;
using TMPro;

namespace Game.Scripts.MenuComponents.ShopComponents.CharacteristicsComponents
{
    [Serializable]
    public class BuffUIConfig
    {
        public BuffType BuffType;
        public Button BuffButton;
        public Button PurchaseButton;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI PriceText;
        public TextMeshProUGUI CostText;
    }
}