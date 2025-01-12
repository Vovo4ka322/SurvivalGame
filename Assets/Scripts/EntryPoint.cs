using PlayerComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CalculationFinalValue _finalValue;
    [SerializeField] private PlayerFactory _playerFactory;
    [SerializeField] private Transform _spawnPoint;

    private Player _player;

    private void Awake()//Переделать логику скинов
    {
        _player = _playerFactory.Get(_finalValue.Skin.PlayerSkin, _spawnPoint);

        _finalValue.Init(_player.Skin);

        _player.Init(_finalValue.CalculateDamage(), _finalValue.CalculateHealth(),
            _finalValue.CalculateArmor(), _finalValue.CalculateAttackSpeed(), _finalValue.CalculateMovementSpeed());
    }
}
