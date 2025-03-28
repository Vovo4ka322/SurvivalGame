using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Scripts.PlayerComponents
{
    public class PlayerDataViewer : MonoBehaviour
    {
        [Header("UI health elements")]
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _healthText;

        [Header("UI elements for experience")]
        [SerializeField] private Slider _expSlider;
        [SerializeField] private TextMeshProUGUI _expText;

        [SerializeField] private TextMeshProUGUI _moneyText;

        private Player _player;

        private void Start()
        {
            if (_player != null)
            {
                InitializeSliders();
                ViewHealth(_player.Health.Value);
                ViewExp(_player.Level.Experience);
            }
        }

        private void OnDisable()
        {
            _player.HealthChanged -= OnHealthChanged;
            _player.ExperienceChanged -= OnExperienceChanged;
            _player.MoneyChanged -= OnMoneyChanged;
            _player.Level.LevelChanged -= OnLevelChanged;
        }

        public void Init(Player player)
        {
            _player = player;

            SubscribeToEvents();
            InitializeSliders();
            ViewHealth(_player.Health.Value);
            ViewExp(_player.Level.Experience);
        }

        private void InitializeSliders()
        {
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = _player.Health.MaxValue;
            _healthSlider.value = _player.Health.Value;

            _expSlider.minValue = 0;
            _expSlider.maxValue = _player.Level.ShowMaxExperienceForLevel();
            _expSlider.value = _player.Level.Experience;
        }

        private void SubscribeToEvents()
        {
            _player.HealthChanged += OnHealthChanged;
            _player.ExperienceChanged += OnExperienceChanged;
            _player.MoneyChanged += OnMoneyChanged;
            _player.Level.LevelChanged += OnLevelChanged;
        }

        private void OnMoneyChanged(int value)
        {
            _moneyText.text = value.ToString();
        }

        private void OnHealthChanged(float value)
        {
            _healthSlider.value = value;

            ViewHealth(value);
        }

        private void OnExperienceChanged(float value)
        {
            _expSlider.value = value;

            ViewExp(value);
        }

        private void OnLevelChanged()
        {
            _expSlider.maxValue = _player.Level.ShowMaxExperienceForLevel();
            ViewExp(_player.Level.Experience);
        }

        private void ViewHealth(float currentHealth)
        {
            _healthText.text = $"{Convert.ToInt32(currentHealth)} / {Convert.ToInt32(_player.Health.MaxValue)}";
        }

        private void ViewExp(float currentExp)
        {
            int maxExp = _player.Level.ShowMaxExperienceForLevel();

            _expText.text = $"{Convert.ToInt32(currentExp)} / {maxExp}";
        }
    }
}