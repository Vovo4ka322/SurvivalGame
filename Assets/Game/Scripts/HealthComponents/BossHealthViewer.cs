using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.EnemyComponents;
using TMPro;

namespace Game.Scripts.HealthComponents
{
    public class BossHealthViewer : MonoBehaviour
    {
        [SerializeField] private Image _healthBarImage;
        [SerializeField] private TextMeshProUGUI _healthText;
        
        private Enemy _boss;
        
        public Enemy Boss => _boss;

        public void Set(Enemy boss)
        {
            if (_boss != null)
            {
                UnsubscribeBossEvents();
            }
        
            _boss = boss;
            
            if (_boss != null)
            {
                UpdateUI(_boss.Health.Value, _boss.Data.MaxHealth);
                
                _boss.Changed += OnHealthChanged;
                _boss.Dead += OnBossDead;
            }
        }
        
        private void UnsubscribeBossEvents()
        {
            if (_boss != null)
            {
                _boss.Changed -= OnHealthChanged;
                _boss.Dead -= OnBossDead;
            }
        }
        
        private void OnHealthChanged(float currentHealth)
        {
            UpdateUI(currentHealth, _boss.Data.MaxHealth);
        }
        
        private void OnBossDead(Enemy boss)
        {
            UnsubscribeBossEvents();
            
            gameObject.SetActive(false);
        }
        
        private void UpdateUI(float currentHealth, float maxHealth)
        {
            if (_healthBarImage != null)
            {
                _healthBarImage.fillAmount = currentHealth / maxHealth;
            }
        
            if (_healthText != null)
            {
                _healthText.text = $"{currentHealth:0}";
            }
        }
    }
}