using System;
using UnityEngine;
using Cinemachine;
using Game.Scripts.EnemyComponents;
using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.WalletComponents;
using Game.Scripts.MusicComponents;
using Game.Scripts.MusicComponents.EffectSounds;
using Game.Scripts.PlayerComponents;
using Game.Scripts.PoolComponents;
using YG;

namespace Game.Scripts.MenuComponents.ShopComponents.GameplaySceneTest
{
    public class GameplayBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _characterSpawnPoint;
        [SerializeField] private GeneralPlayerFactory _generalPlayerFactory;
        [SerializeField] private CanvasFactory _canvasFactory;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private WaveBasedEnemySpawner _enemySpawner;
        [SerializeField] private PoolManager _pool;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private SoundCollection _soundCollection;
        [SerializeField] private AudioGameSettings _audioGameSettings;
        [SerializeField] private GameSceneAudio _gameSceneAudio;
        [SerializeField] private WaveCycle _waveCycle;

        private Player _player;
        private Canvas _canvas;
        private Wallet _wallet;
        private WalletView _walletView;
        private GameplayMenu _gameplayMenu;
        private bool _isJoystickActive;

        private IDataSaver _iDataSaver;
        private IPersistentData _persistentPlayerData;

        private void Awake()
        {
            if (YandexGame.EnvironmentData.isDesktop)
            {
                _isJoystickActive = false;
            }
            else if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
            {
                _isJoystickActive = true;
            }

            InitializeData();
            Spawn();

            _enemySpawner.Init(_player);

        }

        private void OnEnable()
        {
            _enemyFactory.BossDead += _gameplayMenu.OnPlayerWon;
        }

        private void OnDisable()
        {
            _enemyFactory.BossDead -= _gameplayMenu.OnPlayerWon;
        }

        private void Spawn()
        {
            _wallet = new(_persistentPlayerData);
            _player = _generalPlayerFactory.Get(_persistentPlayerData.PlayerData.SelectedCharacterSkin, _characterSpawnPoint.position);

            if (_player == null)
            {
                throw new ArgumentException("The player is not created! Check GeneralPlayerFactory and PersistentData.");
            }

            _virtualCamera.Follow = _player.transform;
            _virtualCamera.LookAt = _player.transform;

            _player.SetSoundCollection(_soundCollection);
            InitPlayerCharacteristics();
            InitUserInterface();

            if (_isJoystickActive)
                _player.InitJoysticks(_isJoystickActive, _canvasFactory.MovementJoystick, _canvasFactory.RotationJoystick);
        }

        private void InitPlayerCharacteristics()
        {
            _player.Init(_persistentPlayerData.PlayerData.CalculationFinalValue.Health, _persistentPlayerData.PlayerData.CalculationFinalValue.Armor,
            _persistentPlayerData.PlayerData.CalculationFinalValue.Damage, _persistentPlayerData.PlayerData.CalculationFinalValue.AttackSpeed,
            _persistentPlayerData.PlayerData.CalculationFinalValue.MovementSpeed, _wallet, _iDataSaver);
        }

        private void InitUserInterface()
        {
            if (_canvasFactory == null)
            {
                throw new ArgumentException("CanvasFactory is not appointed at GameplayTestbootstrap.");
            }

            _canvas = _canvasFactory.Create(_player.CharacterType, _player);

            if (_isJoystickActive)
                _canvasFactory.Init(_isJoystickActive);

            if (_canvas == null)
            {
                throw new ArgumentException("The creation of Canvas ended with an error.");
            }

            GameplayMenu menu = _canvas.GetComponent<GameplayMenu>();
            _gameplayMenu = menu;

            if (menu != null)
            {
                menu.Init(_player);
                menu.SetAudioGameSettings(_audioGameSettings);
                menu.SetGameSceneAudio(_gameSceneAudio);
                _enemySpawner.SetBossHealthViewer(menu.BossHealthViewer);
                menu.WaveTimerUI?.SetWaveCycle(_waveCycle);
            }
            else
            {
                throw new ArgumentException("The GameplayMenu component was not found on the created canvas.");
            }

            _walletView = _canvas.GetComponentInChildren<WalletView>();
            _walletView.Initialize(_wallet);
        }

        private void InitializeData()
        {
            _persistentPlayerData = new PersistentData();
            _iDataSaver = new IDataLocalSaver(_persistentPlayerData);

            LoadDataOrInit();
        }

        private void LoadDataOrInit()
        {
            if (_iDataSaver.TryLoad() == false)
                _persistentPlayerData.PlayerData = new PlayerData();
        }
    }
}