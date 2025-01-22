using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("ButtonsInMainMenu")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;

    [Header("Return buttons")]
    [SerializeField] private List<Button> _returnToMainMenuButton;
    [SerializeField] private List<Button> _returnsToChoicePanel;

    [Header("Choise player buttond")]
    [SerializeField] private Button _meleePlayerButton;
    [SerializeField] private Button _rangePlayerButton;

    [Header("Description panels")]
    [SerializeField] private Image _meleePlayerGame;
    [SerializeField] private Image _rangePlayerGame;

    [Header("Shop")]
    [SerializeField] private GameObject _shop;
    [SerializeField] private SkinPlacement _skinPlacement;

    [Header("Elements of menu")]
    [SerializeField] private Image _choicePlayerPanel;
    [SerializeField] private GameObject _menu;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(Play);
        _meleePlayerButton.onClick.AddListener(ChooseMeleePlayer);
        _rangePlayerButton.onClick.AddListener(ChooseRangePlayer);
        _shopButton.onClick.AddListener(OpenShop);

        for (int i = 0; i < _returnToMainMenuButton.Count; i++)
        {
            _returnToMainMenuButton[i].onClick.AddListener(ComeBackToMainMenu);
        }

        for (int i = 0; i < _returnsToChoicePanel.Count; i++)
        {
            _returnsToChoicePanel[i].onClick.AddListener(ComeBackToChoicePlayer);
        }
    }

    private void OnDisable()
    {
        _playButton?.onClick.RemoveListener(Play);
        _meleePlayerButton.onClick?.RemoveListener(ChooseMeleePlayer);
        _rangePlayerButton?.onClick?.RemoveListener(ChooseRangePlayer);
        _shopButton.onClick.RemoveListener(OpenShop);

        for (int i = 0; i < _returnToMainMenuButton.Count; i++)
        {
            _returnToMainMenuButton[i].onClick.RemoveListener(ComeBackToMainMenu);
        }

        for (int i = 0; i < _returnsToChoicePanel.Count; i++)
        {
            _returnsToChoicePanel[i].onClick.RemoveListener(ComeBackToChoicePlayer);
        }
    }

    private void Play()
    {
        _choicePlayerPanel.gameObject.SetActive(true);
    }

    private void ComeBackToMainMenu()
    {
        _choicePlayerPanel.gameObject.SetActive(false);

        _menu.gameObject.SetActive(true);

        _shop.gameObject.SetActive(false);
    }

    private void ComeBackToChoicePlayer()
    {
        _meleePlayerGame.gameObject.SetActive(false);
        _rangePlayerGame.gameObject.SetActive(false);
    }

    private void ChooseMeleePlayer()
    {
        _meleePlayerGame.gameObject.SetActive(true);
    }
     
    private void ChooseRangePlayer()
    {
        _rangePlayerGame.gameObject.SetActive(true);
    }

    private void OpenShop()
    {
        _menu.gameObject.SetActive(false);
        _shop.gameObject.SetActive(true);
        _skinPlacement.gameObject.SetActive(true);
    }
}