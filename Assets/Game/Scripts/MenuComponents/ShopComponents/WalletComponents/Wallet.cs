using System;
using Game.Scripts.MenuComponents.ShopComponents.Data;

namespace Game.Scripts.MenuComponents.ShopComponents.WalletComponents
{
    public class Wallet
    {
        private readonly IPersistentData _persistentData;
        
        public event Action<int> CoinsChanged;
        
        public Wallet(IPersistentData persistentData) => _persistentData = persistentData;

        public void AddCoins(int coins)
        {
            if(coins < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(coins));
            }
            
            _persistentData.PlayerData.Money += coins;
            
            CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
        }
        
        public int GetCurrentCoins() => _persistentData.PlayerData.Money;
        
        public bool IsEnough(int coins)
        {
            if(coins < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(coins));
            }
            
            return _persistentData.PlayerData.Money >= coins;
        }
        
        public void Spend(int coins)
        {
            if(coins < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(coins));
            }
            
            _persistentData.PlayerData.Money -= coins;
            
            CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
        }
    }
}