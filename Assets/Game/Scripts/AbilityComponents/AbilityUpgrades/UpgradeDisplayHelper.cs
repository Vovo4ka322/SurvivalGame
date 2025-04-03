using System;

namespace Game.Scripts.AbilityComponents.AbilityUpgrades
{
    public class UpgradeDisplayHelper
    {
        public void UpdateUpgradeDisplay(
            UpgradeTextDisplay display,
            int currentLevel,
            Func<AbilitySet, float> valueSelector,
            Func<int, AbilitySet> getAbilityDataForLevel)
        {
            if (currentLevel == 0)
            {
                AbilitySet nextData = getAbilityDataForLevel(1);

                if (nextData != null)
                {
                    display.SetText(0, valueSelector(nextData));
                }
            }
            else
            {
                AbilitySet currentData = getAbilityDataForLevel(currentLevel);
                AbilitySet nextData = getAbilityDataForLevel(currentLevel + 1);

                if (currentData != null)
                {
                    float currentValue = valueSelector(currentData);
                    float? nextValue = nextData != null ? (float?)valueSelector(nextData) : null;

                    display.SetText(currentValue, nextValue);
                }
            }
        }
    }
}