using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("ButtonsInMainMenu")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingsButton;

    [Header("Return buttons")]
    [SerializeField] private Button _returnToMainMenuButton;
    [SerializeField] private List<Button> _returnsToChoicePanel;

    [Header("Choise player buttond")]
    [SerializeField] private Button _meleePlayerButton;
    [SerializeField] private Button _rangePlayerButton;

    [Header("Description panels")]
    [SerializeField] private Image _meleePlayerGame;
    [SerializeField] private Image _rangePlayerGame;

    [Header("Play")]
    [SerializeField] private List<Button> _playButtons;

    [SerializeField] private Image _choicePlayerPanel;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(Play);
        _returnToMainMenuButton.onClick.AddListener(ComeBackToMainMenu);
        _meleePlayerButton.onClick.AddListener(ChooseMeleePlayer);
        _rangePlayerButton.onClick.AddListener(ChooseRangePlayer);

        for (int i = 0; i < _returnsToChoicePanel.Count; i++)
        {
            _returnsToChoicePanel[i].onClick.AddListener(ComeBackToChoicePlayer);
        }
    }

    private void OnDisable()
    {
        _playButton?.onClick.RemoveListener(Play);
        _returnToMainMenuButton.onClick.RemoveListener(ComeBackToMainMenu);
        _meleePlayerButton.onClick?.RemoveListener(ChooseMeleePlayer);
        _rangePlayerButton?.onClick?.RemoveListener(ChooseRangePlayer);

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

    private void StartGame()
    {

    }
}