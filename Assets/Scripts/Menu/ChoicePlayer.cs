using PlayerComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoicePlayer : MonoBehaviour
{
    //[SerializeField] private Image _imageMeleePlayer;
    //[SerializeField] private Image _imageRangePlayer;

    [SerializeField] private Button _meleeCharacter;
    [SerializeField] private Button _rangeCharacter;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    private void OnEnable()
    {
        _meleeCharacter.onClick.AddListener(OnMeleeCharacterChosen);
        _rangeCharacter.onClick.AddListener(OnRangeCharacterChosen);
    }

    private void OnDisable()
    {
        _meleeCharacter.onClick.RemoveListener(OnMeleeCharacterChosen);
        _rangeCharacter.onClick.RemoveListener(OnRangeCharacterChosen);
    }

    public void OnMeleeCharacterChosen()
    {
        

        ChangeScene();

       
    }

    public void OnRangeCharacterChosen()
    {
        ChangeScene();

    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }

    //private void InitializeData()
    //{
    //    _persistentPlayerData = new PersistentData();
    //    _dataProvider = new DataLocalProvider(_persistentPlayerData);

    //    LoadDataOrInit();
    //}

    //private void LoadDataOrInit()
    //{
    //    if (_dataProvider.TryLoad() == false)
    //        _persistentPlayerData.PlayerData = new PlayerData();
    //}
}
