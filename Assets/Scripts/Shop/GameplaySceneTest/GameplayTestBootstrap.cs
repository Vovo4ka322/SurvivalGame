using Cinemachine;
using EnemyComponents;
using PlayerComponents;
using UnityEngine;
using UnityEngine.UI;

public class GameplayTestBootstrap : MonoBehaviour
{
    [SerializeField] private Transform _characterSpawnPoint;
    [SerializeField] private GeneralPlayerFactory _generalPlayerFactory;
    [SerializeField] private CanvasFactory _canvasFactory;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private WaveBasedEnemySpawner _enemySpawner;

    private Player _player;
    private Canvas _canvas;

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
        _player = _generalPlayerFactory.Get(_persistentPlayerData.PlayerData.SelectedCharacterSkin, _characterSpawnPoint.position);

        _virtualCamera.Follow = _player.transform;
        _virtualCamera.LookAt = _player.transform;

        InitPlayerCharacteristics();
    }

    private void InitPlayerCharacteristics()
    {
        _player.Init(_persistentPlayerData.PlayerData.CalculationFinalValue.Health, _persistentPlayerData.PlayerData.CalculationFinalValue.Armor,
        _persistentPlayerData.PlayerData.CalculationFinalValue.Damage, _persistentPlayerData.PlayerData.CalculationFinalValue.AttackSpeed, _persistentPlayerData.PlayerData.CalculationFinalValue.MovementSpeed);
        _canvas = _canvasFactory.Create(_player.CharacterType, _player);

        //Debug.Log(_persistentPlayerData.PlayerData.CalculationFinalValue.Health + " health при спавне");
        //Debug.Log(_persistentPlayerData.PlayerData.CalculationFinalValue.Armor + " armor при спавне");
        //Debug.Log(_persistentPlayerData.PlayerData.CalculationFinalValue.Damage + " damage при спавне");
        //Debug.Log(_persistentPlayerData.PlayerData.CalculationFinalValue.AttackSpeed + " attack при спавне");
        //Debug.Log(_persistentPlayerData.PlayerData.CalculationFinalValue.MovementSpeed + " movement при спавне");
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