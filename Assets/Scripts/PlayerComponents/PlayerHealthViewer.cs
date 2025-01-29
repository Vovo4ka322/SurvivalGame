using PlayerComponents;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthViewer : MonoBehaviour
{
    [SerializeField] private Image _healthValueImage;

    private Player _player;

    private void OnEnable() => _player.HealthChanged += OnChanged;

    private void OnDisable() => _player.HealthChanged -= OnChanged;

    public void Init(Player player) => _player = player;

    private void OnChanged(float value) =>
        _healthValueImage.fillAmount = Mathf.InverseLerp(0, _player.Health.MaxValue, value);
}