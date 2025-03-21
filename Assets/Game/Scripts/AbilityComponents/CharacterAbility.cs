using UnityEngine;
using Game.Scripts.Interfaces;

namespace Game.Scripts.AbilityComponents
{
    public class CharacterAbility : ScriptableObject, IAbilitable
    {
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField] public string Description { get; private set; }
    }
}