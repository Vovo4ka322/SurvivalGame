using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace Game.Scripts.PlayerComponents
{
    public class PlayerDataViewer : MonoBehaviour
    {
        [SerializeField] private Image _healthValueImage;
        [SerializeField] private Image _ExpValueImage;

        [SerializeField] private Text _moneyText;
        [SerializeField] private Text _healthText;

        private Player _player;

        private void Start()
        {
            ViewHealth(_player.Health.MaxValue);
        }

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

        private void OnHealthChanged(float value)
        {
            _healthValueImage.fillAmount = Mathf.InverseLerp(0, _player.Health.MaxValue, value);
            ViewHealth(value);
        }

        private void ViewHealth(float value) => _healthText.text = value.ToString();

        private void OnExperienceChanged(float value) 
            => _ExpValueImage.fillAmount = Mathf.InverseLerp(0, _player.Level.ShowMaxExperienceForLevel(), value);
    }
}