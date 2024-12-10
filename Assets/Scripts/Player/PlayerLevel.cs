using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerLevel
{
    private Dictionary<int, int> _levelRequirements;

    public event Action LevelChanged;

    public PlayerLevel()
    {
        _levelRequirements = new Dictionary<int, int>
        {
            { 1, 100},
            { 2, 150},
            { 3, 220},
            { 4, 350},
            { 5, 440}
        };
    }

    [field: SerializeField] public int Level {  get; private set; }

    [field: SerializeField] public int Experience {  get; private set; }


    public void GainExperience(int amount)
    {
        Experience += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (_levelRequirements.TryGetValue(Level + 1, out int requiredExperience))
        {
            while(Experience >= requiredExperience)
            {
                Level++;
                LevelChanged?.Invoke();
                requiredExperience = _levelRequirements[Level + 1];
            }
        }
    }
}
