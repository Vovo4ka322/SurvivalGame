using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerComponents
{
    [Serializable]
    public class PlayerLevel
    {
        [SerializeField] private Level _requireExperience;

        private int _level;
        private Dictionary<int, int> _levelRequirements;

        public event Action LevelChanged;

        public int Experience { get; private set; }

        public void Init()
        {
            _levelRequirements = new Dictionary<int, int>();

            for (int i = 0; i < _requireExperience.ExperienceQuntity.Count; i++)
            {
                _levelRequirements.Add(i + 1, _requireExperience.ExperienceQuntity[i]);
            }
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            UpLevel();
        }

        private void UpLevel()
        {
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