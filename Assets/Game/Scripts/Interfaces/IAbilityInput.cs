using UnityEngine.UI;

namespace Game.Scripts.Interfaces
{
    public interface IAbilityInput
    {
        void Init(Button firstAbilityUse, Button secondAbilityUse, Button firstUpgradeButton, Button secondUpgradeButton, Button thirdUpgradeButton);
    }
}