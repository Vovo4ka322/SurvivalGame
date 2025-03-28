using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PlayerComponents
{
    [Serializable]
    public class PlayerLevel
    {
        [SerializeField] private Level _requireExperience;

        private Dictionary<int, int> _levelRequirements;

        private int _level;
        private int _maxLevel = 9;

        public event Action LevelChanged;

        [field: SerializeField] public int Experience { get; private set; }

        public void Init()
        {
            _levelRequirements = new Dictionary<int, int>();

            for (int i = 0; i < _requireExperience.ExperienceQuantity.Count; i++)
            {
                _levelRequirements.Add(i + 1, _requireExperience.ExperienceQuantity[i]);
            }
        }

        public int ShowMaxExperienceForLevel() => _requireExperience.ExperienceQuantity[_level];

        public void GainExperience(int amount)
        {
            Experience += amount;

            UpLevel();
        }

        private void UpLevel()
        {
            if (_level >= _maxLevel)
            {
                return;
            }

            if (_levelRequirements.TryGetValue(_level + 1, out int requiredExperience))
            {
                while (Experience >= requiredExperience)
                {
                    _level++;
                    Experience = 0;

                    LevelChanged?.Invoke();

                    requiredExperience = _levelRequirements[_level + 1];
                }
            }
        }
    }
}