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

    public event Action MeleePlayerChosen;
    public event Action RangePlayerChosen;

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
        MeleePlayerChosen?.Invoke();
    }

    public void OnRangeCharacterChosen()
    {
        ChangeScene();
        RangePlayerChosen?.Invoke();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}
