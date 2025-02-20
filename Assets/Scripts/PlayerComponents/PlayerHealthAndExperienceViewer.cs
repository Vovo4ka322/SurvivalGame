using PlayerComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthAndExperienceViewer : MonoBehaviour
{
    [SerializeField] private Image _healthValueImage;
    [SerializeField] private Image _ExpValueImage;

    [SerializeField] private Button _getExp;
    [SerializeField] private Button _loseHP;

    [SerializeField] private TextMeshProUGUI _moneyText;

    private Player _player;

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
        _player.ExperienceChanged -= OnExperienceChanged;
        _player.MoneyChanged -= OnMoneyChanged;
    }

    public void Init(Player player)
    {
        _player = player;

        SubsctibeToEvents();
    }

    private void SubsctibeToEvents()
    {
        _player.HealthChanged += OnHealthChanged;
        _player.ExperienceChanged += OnExperienceChanged;
        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnMoneyChanged(int value) => _moneyText.text = value.ToString();

    private void OnHealthChanged(float value) =>
        _healthValueImage.fillAmount = Mathf.InverseLerp(0, _player.Health.MaxValue, value);

    private void OnExperienceChanged(float value) =>
       _ExpValueImage.fillAmount = Mathf.InverseLerp(_player.Level.Experience, 0, value);
}