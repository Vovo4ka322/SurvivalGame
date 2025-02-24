using UnityEngine;
using MenuComponents.ShopComponents.Data;
using MenuComponents.ShopComponents.Viewers;
using MenuComponents.ShopComponents.Visitors;
using MenuComponents.ShopComponents.WalletComponents;
using Music;

namespace MenuComponents.ShopComponents
{
    public class ShopBootstrap : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private BuffImprovmentViewer _improvmentViewer;
        [SerializeField] private SoundMixerSettings _soundMixerSettings;

        private IDataProvider _dataProvider;
        private IPersistentData _persistentPlayerData;
    
        private Wallet _wallet;
        private PlayerCharacteristicData _calculationFinalValue;
    
        private void Awake()
        {
            InitializeData();
            InitializeWallet();
            InitializeImprovmentViewer();
            InitializeShop();
        }

        private void Start()
        {
            InitializeSound();
        }

        private void InitializeSound()
        {
            _soundMixerSettings.Initialize();
        }

        private void InitializeData()
        {
            _persistentPlayerData = new PersistentData();
            _dataProvider = new DataLocalProvider(_persistentPlayerData);
    
            LoadDataOrInit();
        }
    
        private void InitializeWallet()
        {
            _wallet = new Wallet(_persistentPlayerData);
            
            _walletView.Initialize(_wallet);
        }
    
        private void InitializeImprovmentViewer()
        {
            _calculationFinalValue = _persistentPlayerData.PlayerData.CalculationFinalValue;
    
            _improvmentViewer.Init(_calculationFinalValue);
        }
    
        private void InitializeShop()
        {
            OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(_persistentPlayerData);
            SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentPlayerData);
            SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
            SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentPlayerData);
    
            _shop.Initialize(_dataProvider, _wallet, openSkinsChecker, selectedSkinChecker, skinSelector, skinUnlocker, _persistentPlayerData);
        }
    
        private void LoadDataOrInit()
        {
            if(_dataProvider.TryLoad() == false)
            {
                _persistentPlayerData.PlayerData = new PlayerData();
            }
        }
    }
}