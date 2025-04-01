using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Scripts.AbilityComponents.AbilityUpgrades
{
    [Serializable]
    public class UpgradeTextDisplay
    {
        [SerializeField] private Image _arrowImage;
        [SerializeField] private TextMeshProUGUI _currentText;
        [SerializeField] private TextMeshProUGUI _nextText;
        [SerializeField] private TextMeshProUGUI _maxValueText;

        public void SetText(float current, float? next)
        {
            if (next.HasValue)
            {
                _currentText.gameObject.SetActive(true);
                _currentText.text = FormatValue(current);
                _nextText.gameObject.SetActive(true);
                _nextText.text = FormatValue(next.Value);
                _arrowImage.gameObject.SetActive(true);
                _maxValueText.gameObject.SetActive(false);
            }
            else
            {
                _currentText.gameObject.SetActive(false);
                _nextText.gameObject.SetActive(false);
                _arrowImage.gameObject.SetActive(false);
                _maxValueText.gameObject.SetActive(true);
            }
        }

        private string FormatValue(float value)
        {
            if (Mathf.Approximately(value, Mathf.Round(value)))
            {
                return Mathf.RoundToInt(value).ToString();
            }
            else
            {
                return value.ToString();
            }
        }
    }
}