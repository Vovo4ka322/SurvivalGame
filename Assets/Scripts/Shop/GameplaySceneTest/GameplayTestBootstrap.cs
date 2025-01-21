using UnityEngine;

public class GameplayTestBootstrap : MonoBehaviour
{
    [SerializeField] private Transform _characterSpawnPoint;
    [SerializeField] private Transform _mazeCellSpawnPoint;
    [SerializeField] private MeleePlayerFactory _characterFactory;
    [SerializeField] private RangePlayerFactory _mazeCellFactory;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    private void Awake()
    {
        InitializeData();

        //DoTestSpawn();
    }

    private void DoTestSpawn()
    {
        Character character = _characterFactory.Get(_persistentPlayerData.PlayerData.SelectedMeleeCharacterSkin, _characterSpawnPoint.position);

        Debug.Log($"«аспавнили персонажа {_persistentPlayerData.PlayerData.SelectedMeleeCharacterSkin} и клетку лабиринта {_persistentPlayerData.PlayerData.SelectedRangeCharacterSkin}");
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
