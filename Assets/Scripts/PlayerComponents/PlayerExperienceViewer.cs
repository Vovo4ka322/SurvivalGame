using PlayerComponents;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperienceViewer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _ExpValueImage;

    private void OnEnable() => _player.ExperienceChanged += OnChanged;

    private void OnDisable() => _player.ExperienceChanged -= OnChanged;

    private void OnChanged(float value) =>
        _ExpValueImage.fillAmount = Mathf.InverseLerp(_player.Level.Experience, 0, value);
}