using Game.Scripts.HealthComponents;
using UnityEngine;

namespace Game.Scripts.PlayerComponents
{
    [RequireComponent(typeof(Health))]
    public abstract class Character : MonoBehaviour
    {
        [field: SerializeField] public Health Health { get; private set; }
    }
}