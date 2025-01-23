using PlayerComponents;
using UnityEngine;
using UnityEngine.UI;

public class GameplayTestBootstrap : MonoBehaviour
{
    [SerializeField] private Transform _characterSpawnPoint;
    [SerializeField] private CalculationFinalValue _finalValue;
    [SerializeField] private MeleePlayerFactory _meleeCharacterFactory;
    [SerializeField] private RangePlayerFactory _rangeCharacterFactory;

    //q
    [SerializeField] private GeneralPlayerFactory _generalPlayerFactory;

    private Player _player;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    private void Awake()
    {
        InitializeData();

        DoTestSpawn();
    }

    private void DoTestSpawn()
    {
        _player = _generalPlayerFactory.Get(_persistentPlayerData.PlayerData.SelectedCharacterSkin, _characterSpawnPoint.position);

        InitPlayer();
    }

    private void InitPlayer()
    {
        _player.Init(_finalValue.CalculateDamage(), _finalValue.CalculateHealth(),
        _finalValue.CalculateArmor(), _finalValue.CalculateAttackSpeed(), _finalValue.CalculateMovementSpeed());
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