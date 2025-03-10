using Game.Scripts.EnemyComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthViewer : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    private void Start()
    {
        Change(_enemy.Data.MaxHealth);
    }

    private void OnEnable()
    {
        _enemy.Changed += OnHealthChange;
    }

    private void OnDisable()
    {
        _enemy.Changed -= OnHealthChange;
    }

    private void OnHealthChange(float value)
    {
        Change(value);
        _image.fillAmount = Mathf.InverseLerp(0, _enemy.Data.MaxHealth, value);
    }

    private void Change(float value) => _text.text = value.ToString();
}
