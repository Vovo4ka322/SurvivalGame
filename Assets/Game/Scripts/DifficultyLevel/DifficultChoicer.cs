using Game.Scripts.DifficultyLevel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultChoicer : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private DifficultySetter _difficultySetter;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

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
    }

    private void ShowText()
    {
        _textMeshProUGUI.text = $"";
    }
}
