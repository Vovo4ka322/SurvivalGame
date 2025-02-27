using UnityEngine;
using Game.Scripts.Interfaces;

namespace Ability
{
    public class Ability : ScriptableObject, IAbilitable
    {
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField] public string Description { get; private set; }
    }
}