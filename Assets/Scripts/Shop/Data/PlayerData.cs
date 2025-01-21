using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerData
{
    private MeleeCharacterSkins _selectedMeleeCharacterSkin;
    private RangeCharacterSkins _selectedRangeCharacterSkin;

    private List<MeleeCharacterSkins> _openMeleeCharacterSkins;
    private List<RangeCharacterSkins> _openRangeCharacterSkins;

    private int _money;

    public PlayerData()
    {
        _money = 30000;

        _selectedMeleeCharacterSkin = MeleeCharacterSkins.FirstMeleeSkin;
        _selectedRangeCharacterSkin = RangeCharacterSkins.FirstRangeSkin;

        _openMeleeCharacterSkins = new List<MeleeCharacterSkins>() { _selectedMeleeCharacterSkin };
        _openRangeCharacterSkins = new List<RangeCharacterSkins>() { _selectedRangeCharacterSkin };
    }

    [JsonConstructor]
    public PlayerData(int money, MeleeCharacterSkins selectedMeleeCharacterSkin, RangeCharacterSkins selectedRangeCharacterSkin,
        List<MeleeCharacterSkins> openMeleeCharacterSkins, List<RangeCharacterSkins> openRangeCharacterSkins)
    {
        Money = money;

        _selectedMeleeCharacterSkin = selectedMeleeCharacterSkin;
        _selectedRangeCharacterSkin = selectedRangeCharacterSkin;

        _openMeleeCharacterSkins = new List<MeleeCharacterSkins>(openMeleeCharacterSkins);
        _openRangeCharacterSkins = new List<RangeCharacterSkins>(openRangeCharacterSkins);
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
        if(_openMeleeCharacterSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openMeleeCharacterSkins.Add(skin);
    }

    public void OpenRangeCharacterSkin(RangeCharacterSkins skin)
    {
        if (_openRangeCharacterSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openRangeCharacterSkins.Add(skin);
    }
}
