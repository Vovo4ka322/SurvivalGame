using PlayerComponents;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperienceViewer : MonoBehaviour
{
    [SerializeField] private Image _ExpValueImage;

    private Player _player;

    private void OnEnable() => _player.ExperienceChanged += OnChanged;

    private void OnDisable() => _player.ExperienceChanged -= OnChanged;

    public void Init(Player player) => _player = player;

    private void OnChanged(float value) =>
        _ExpValueImage.fillAmount = Mathf.InverseLerp(_player.Level.Experience, 0, value);
}