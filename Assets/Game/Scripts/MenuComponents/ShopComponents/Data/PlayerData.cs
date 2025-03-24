using System;
using System.Collections.Generic;
using Game.Scripts.BuffComponents;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;
using Newtonsoft.Json;

namespace Game.Scripts.MenuComponents.ShopComponents.Data
{
    public class PlayerData
    {
        private readonly List<CharacterSkins> _openCharacterSkins;
        private readonly PlayerCharacteristicData _calculationFinalValue;
        
        private CharacterSkins _selectedCharacterSkins;
        
        private int _money;
        
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
        
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static PlayerData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<PlayerData>(json);
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