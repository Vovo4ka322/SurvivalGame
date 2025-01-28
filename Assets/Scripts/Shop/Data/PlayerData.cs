using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerData
{
    private CharacterSkins _selectedCharacterSkins;

    private List<CharacterSkins> _openCharacterSkins;

    private int _money;

    private CalculationFinalValue _calculationFinalValue;

    public PlayerData()
    {
        _money = 30000;

        _calculationFinalValue = new CalculationFinalValue();

        _selectedCharacterSkins = CharacterSkins.FirstMeleeSkin;

        _openCharacterSkins = new() { _selectedCharacterSkins };
    }

    [JsonConstructor]
    public PlayerData(int money, CharacterSkins characterSkins, List<CharacterSkins> openCharacterSkins, CalculationFinalValue calculationFinalValue)
    {
        Money = money;

        _selectedCharacterSkins = characterSkins;
        _openCharacterSkins = new(openCharacterSkins);
        _calculationFinalValue = calculationFinalValue;

        Debug.Log(money);
        Debug.Log(calculationFinalValue.Health);
        Debug.Log(calculationFinalValue.Armor);
        Debug.Log(calculationFinalValue.Damage);
        Debug.Log(calculationFinalValue.AttackSpeed);
        Debug.Log(calculationFinalValue.MovementSpeed);
    }

    public int Money
    {
        get => _money;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _money = value;
        }
    }

    public void OpenCharacterSkin(CharacterSkins skin)
    {
        if (_openCharacterSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openCharacterSkins.Add(skin);
    }

    public IEnumerable<CharacterSkins> OpenCharacterSkins => _openCharacterSkins;

    public CalculationFinalValue CalculationFinalValue => _calculationFinalValue;

    public CharacterSkins SelectedCharacterSkin
    {
        get => _selectedCharacterSkins;
        set
        {
            if (_openCharacterSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedCharacterSkins = value;
        }
    }
}
