using Game.Scripts.DifficultyLevel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyChoicer : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameDifficultySetter _difficultySetter;

    [SerializeField] private TextMeshProUGUI _easyDifficultyText;
    [SerializeField] private TextMeshProUGUI _mediumDifficultyText;
    [SerializeField] private TextMeshProUGUI _hardDifficultyText;

    [SerializeField] private Difficults _difficults;

    private void OnEnable()
    {
        _button.onClick.AddListener(SetDifficultsEnemy);
    }

    private void OnDisable()
    {
        _button.onClick?.RemoveListener(SetDifficultsEnemy);
    }

    private void SetDifficultsEnemy()
    {
        _difficultySetter.SetDifficult(_difficults);
        ShowText();
    }

    private void ShowText()
    {
        if (_difficults == Difficults.Easy)
        {
            Deactivate();
            Activate(_easyDifficultyText);
        }
        else if (_difficults == Difficults.Medium)
        {
            Deactivate();
            Activate(_mediumDifficultyText);
        }
        else if (_difficults == Difficults.Hard)
        {
            Deactivate();
            Activate(_hardDifficultyText);
        }
    }

    private void Deactivate()
    {
        _easyDifficultyText.gameObject.SetActive(false);
        _mediumDifficultyText.gameObject.SetActive(false);
        _hardDifficultyText.gameObject.SetActive(false);
    }

    private void Activate(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
    }
}
