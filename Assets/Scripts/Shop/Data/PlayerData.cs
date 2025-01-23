using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerData
{
    private MeleeCharacterSkins _selectedMeleeCharacterSkin;
    private RangeCharacterSkins _selectedRangeCharacterSkin;

    //q
    private CharacterSkins _selectedCharacterSkins;

    private List<MeleeCharacterSkins> _openMeleeCharacterSkins;
    private List<RangeCharacterSkins> _openRangeCharacterSkins;

    //q
    private List<CharacterSkins> _openCharacterSkins;

    private int _money;

    public PlayerData()
    {
        _money = 30000;

        _selectedMeleeCharacterSkin = MeleeCharacterSkins.FirstMeleeSkin;
        _selectedRangeCharacterSkin = RangeCharacterSkins.FirstRangeSkin;

        _selectedCharacterSkins = CharacterSkins.FirstMeleeSkin;

        _openMeleeCharacterSkins = new List<MeleeCharacterSkins>() { _selectedMeleeCharacterSkin };
        _openRangeCharacterSkins = new List<RangeCharacterSkins>() { _selectedRangeCharacterSkin };

        //q
        _openCharacterSkins = new() { _selectedCharacterSkins };
    }

    [JsonConstructor]
    public PlayerData(int money, MeleeCharacterSkins selectedMeleeCharacterSkin, RangeCharacterSkins selectedRangeCharacterSkin,
        List<MeleeCharacterSkins> openMeleeCharacterSkins, List<RangeCharacterSkins> openRangeCharacterSkins, CharacterSkins characterSkins, List<CharacterSkins> openCharacterSkins)
    {
        Money = money;

        _selectedMeleeCharacterSkin = selectedMeleeCharacterSkin;
        _selectedRangeCharacterSkin = selectedRangeCharacterSkin;

        _openMeleeCharacterSkins = new List<MeleeCharacterSkins>(openMeleeCharacterSkins);
        _openRangeCharacterSkins = new List<RangeCharacterSkins>(openRangeCharacterSkins);

        //q
        _selectedCharacterSkins = characterSkins;
        _openCharacterSkins = new(openCharacterSkins);
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

    public MeleeCharacterSkins SelectedMeleeCharacterSkin
    {
        get => _selectedMeleeCharacterSkin;
        set
        {
            if (_openMeleeCharacterSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedMeleeCharacterSkin = value;
        }
    }

    public RangeCharacterSkins SelectedRangeCharacterSkin
    {
        get => _selectedRangeCharacterSkin;
        set
        {
            if (_openRangeCharacterSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedRangeCharacterSkin = value;
        }
    }

    public IEnumerable<MeleeCharacterSkins> OpenMeleeCharacterSkins => _openMeleeCharacterSkins;

    public IEnumerable<RangeCharacterSkins> OpenRangeCharacterSkins => _openRangeCharacterSkins;

    public void OpenMeleeCharacterSkin(MeleeCharacterSkins skin)
    {
        if (_openMeleeCharacterSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openMeleeCharacterSkins.Add(skin);
    }

    public void OpenRangeCharacterSkin(RangeCharacterSkins skin)
    {
        if (_openRangeCharacterSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openRangeCharacterSkins.Add(skin);
    }

    //q
    public void OpenCharacterSkin(CharacterSkins skin)
    {
        if (_openCharacterSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openCharacterSkins.Add(skin);
    }

    public IEnumerable<CharacterSkins> OpenCharacterSkins => _openCharacterSkins;

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
