using PlayerComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CalculationFinalValue _finalValue;

    private void Awake()
    {
        //_player.Init(_finalValue.CalculateDamage(), _finalValue.CalculateHealth(), 
            //_finalValue.CalculateArmor(), _finalValue.CalculateAttackSpeed(), _finalValue.CalculateMovementSpeed());
    }
}
