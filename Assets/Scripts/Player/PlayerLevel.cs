using System;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    [Serializable]
    public class PlayerLevel
    {
        [SerializeField] private Level _requireExperience;

        private Dictionary<int, int> _levelRequirements;

        public event Action LevelChanged;

        [field: SerializeField] public int Level { get; private set; }

        [field: SerializeField] public int Experience { get; private set; }

        public void Init()
        {
            _levelRequirements = new Dictionary<int, int>();

            for (int i = 0; i < _requireExperience.ExperienceQunttity.Count; i++)
            {
                _levelRequirements.Add(i + 1, _requireExperience.ExperienceQunttity[i]);
            }
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            UpLevel();
        }

        private void UpLevel()
        {
            if (_levelRequirements.TryGetValue(Level + 1, out int requiredExperience))
            {
                while (Experience >= requiredExperience)
                {
                    Level++;
                    LevelChanged?.Invoke();
                    requiredExperience = _levelRequirements[Level + 1];
                }
            }
        }
    }
}