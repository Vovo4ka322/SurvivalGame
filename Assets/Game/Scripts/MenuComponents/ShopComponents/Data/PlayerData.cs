using System;
using System.Collections.Generic;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;
using Newtonsoft.Json;

namespace Game.Scripts.MenuComponents.ShopComponents.Data
{
    public class PlayerData
    {
        private CharacterSkins _selectedCharacterSkins;

        private List<CharacterSkins> _openCharacterSkins;

        private int _money;

        private PlayerCharacteristicData _calculationFinalValue;

        public PlayerData()
        {
            _money = 0;

            _calculationFinalValue = new PlayerCharacteristicData();

            _selectedCharacterSkins = CharacterSkins.FirstMeleeSkin;

            _openCharacterSkins = new() { _selectedCharacterSkins };
        }

        [JsonConstructor]
        public PlayerData(int money, CharacterSkins characterSkins, List<CharacterSkins> openCharacterSkins, PlayerCharacteristicData calculationFinalValue)
        {
            Money = money;

            _selectedCharacterSkins = characterSkins;
            _openCharacterSkins = new(openCharacterSkins);
            _calculationFinalValue = calculationFinalValue;
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

        public PlayerCharacteristicData CalculationFinalValue => _calculationFinalValue;

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
}