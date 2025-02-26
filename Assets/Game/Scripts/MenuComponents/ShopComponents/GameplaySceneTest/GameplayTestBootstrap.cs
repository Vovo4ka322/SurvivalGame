using UnityEngine;
using Cinemachine;
using EnemyComponents;
using MenuComponents.ShopComponents.Data;
using MenuComponents.ShopComponents.WalletComponents;
using PlayerComponents;

namespace MenuComponents.ShopComponents.GameplaySceneTest
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

            _virtualCamera.Follow = _player.transform;
            _virtualCamera.LookAt = _player.transform;

            InitPlayerCharacteristics();
            InitUserInterface();
        }

        private void InitPlayerCharacteristics()
        {
            _player.Init(_persistentPlayerData.PlayerData.CalculationFinalValue.Health, _persistentPlayerData.PlayerData.CalculationFinalValue.Armor,
            _persistentPlayerData.PlayerData.CalculationFinalValue.Damage, _persistentPlayerData.PlayerData.CalculationFinalValue.AttackSpeed,
            _persistentPlayerData.PlayerData.CalculationFinalValue.MovementSpeed, _wallet);
        }

        private void InitUserInterface()
        {
            _canvas = _canvasFactory.Create(_player.CharacterType, _player);
            _canvas.GetComponent<GameplayMenu>().Init(_player);
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
