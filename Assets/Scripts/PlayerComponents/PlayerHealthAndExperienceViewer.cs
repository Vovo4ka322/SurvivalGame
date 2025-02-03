using PlayerComponents;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthAndExperienceViewer : MonoBehaviour
{
    [SerializeField] private Image _healthValueImage;
    [SerializeField] private Image _ExpValueImage;

    [SerializeField] private Button _getExp;
    [SerializeField] private Button _loseHP;

    private Player _player;

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
        _player.ExperienceChanged -= OnExperienceChanged;
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
        _getExp.onClick.AddListener(GetExp);
        _loseHP.onClick.AddListener(LoseHealth);
    }

    private void LoseHealth()
    {
        _player.LoseHealth(50);
    }

    private void GetExp()
    {
        _player.GetExperience(50);
    }

    private void OnHealthChanged(float value) =>
        _healthValueImage.fillAmount = Mathf.InverseLerp(0, _player.Health.MaxValue, value);

    private void OnExperienceChanged(float value) =>
       _ExpValueImage.fillAmount = Mathf.InverseLerp(_player.Level.Experience, 0, value);
}