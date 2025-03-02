using UnityEngine;
using Cinemachine;
using Game.Scripts.EnemyComponents;
using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.WalletComponents;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.MenuComponents.ShopComponents.GameplaySceneTest
{
    public class GameplayTestBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _characterSpawnPoint;
        [SerializeField] private GeneralPlayerFactory _generalPlayerFactory;
        [SerializeField] private CanvasFactory _canvasFactory;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private WaveBasedEnemySpawner _enemySpawner;

        private Player _player;
        private Canvas _canvas;
        private Wallet _wallet;
        private WalletView _walletView;

        private IDataProvider _dataProvider;
        private IPersistentData _persistentPlayerData;

        private void Awake()
        {
            InitializeData();

            DoTestSpawn();

            _enemySpawner.Init(_player);
        }

        private void DoTestSpawn()
        {
            _wallet = new(_persistentPlayerData);
            _player = _generalPlayerFactory.Get(_persistentPlayerData.PlayerData.SelectedCharacterSkin, _characterSpawnPoint.position);
            
            if (_player == null)
            {
                Debug.LogError("The player is not created! Check GeneralPlayerFactory and PersistentData.");
                return;
            }
            
            _virtualCamera.Follow = _player.transform;
            _virtualCamera.LookAt = _player.transform;

            InitPlayerCharacteristics();
            InitUserInterface();
        }

        private void InitPlayerCharacteristics()
        {
            _player.Init(_persistentPlayerData.PlayerData.CalculationFinalValue.Health, _persistentPlayerData.PlayerData.CalculationFinalValue.Armor,
            _persistentPlayerData.PlayerData.CalculationFinalValue.Damage, _persistentPlayerData.PlayerData.CalculationFinalValue.AttackSpeed,
            _persistentPlayerData.PlayerData.CalculationFinalValue.MovementSpeed, _wallet, _dataProvider);
        }

        private void InitUserInterface()
        {
            if (_canvasFactory == null)
            {
                Debug.LogError("CanvasFactory is not appointed at GameplayTestbootstrap.");
                return;
            }

            _canvas = _canvasFactory.Create(_player.CharacterType, _player);
            if (_canvas == null)
            {
                Debug.LogError("The creation of Canvas ended with an error.");
                return;
            }

            GameplayMenu menu = _canvas.GetComponent<GameplayMenu>();
            if (menu != null)
                menu.Init(_player);
            else
                Debug.LogError("The GameplayMenu component was not found on the created canvas.");

            _walletView = _canvas.GetComponentInChildren<WalletView>();
            _walletView.Initialize(_wallet);
        }

        private void InitializeData()
        {
            _persistentPlayerData = new PersistentData();
            _dataProvider = new DataLocalProvider(_persistentPlayerData);

            LoadDataOrInit();
        }

        private void LoadDataOrInit()
        {
            if (_dataProvider.TryLoad() == false)
                _persistentPlayerData.PlayerData = new PlayerData();
        }
    }
}
