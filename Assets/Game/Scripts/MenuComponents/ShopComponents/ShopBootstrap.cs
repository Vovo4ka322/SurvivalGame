using UnityEngine;
using Game.Scripts.MenuComponents.ShopComponents.CharacteristicsComponents;
using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.Visitors;
using Game.Scripts.MenuComponents.ShopComponents.WalletComponents;
using Game.Scripts.BuffComponents;

namespace Game.Scripts.MenuComponents.ShopComponents
{
    public class ShopBootstrap : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private BuffImprovementViewer _improvementViewer;

        private IDataSaver _iDataSaver;
        private IPersistentData _persistentPlayerData;

        private Wallet _wallet;
        private PlayerCharacteristicData _calculationFinalValue;

        private void Awake()
        {
            InitializeData();
            InitializeWallet();
            InitializeImprovementViewer();
            InitializeShop();
        }

        private void InitializeData()
        {
            _persistentPlayerData = new PersistentData();
            _iDataSaver = new IDataLocalSaver(_persistentPlayerData);

            LoadDataOrInit();
        }

        private void InitializeWallet()
        {
            _wallet = new Wallet(_persistentPlayerData);

            _walletView.Initialize(_wallet);
        }

        private void InitializeImprovementViewer()
        {
            _calculationFinalValue = _persistentPlayerData.PlayerData.CalculationFinalValue;

            _improvementViewer.Init(_calculationFinalValue);
        }

        private void InitializeShop()
        {
            OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(_persistentPlayerData);
            SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentPlayerData);
            SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
            SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentPlayerData);

            _shop.Initialize(_iDataSaver, _wallet, openSkinsChecker, selectedSkinChecker, skinSelector, skinUnlocker, _persistentPlayerData);
        }

        private void LoadDataOrInit()
        {
            if (_iDataSaver.TryLoad() == false || _persistentPlayerData.PlayerData == null)
            {
                _persistentPlayerData.PlayerData = new PlayerData();
            }
        }
    }
}