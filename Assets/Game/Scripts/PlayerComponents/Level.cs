using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PlayerComponents
{
    [CreateAssetMenu(fileName = "PlayerLevel")]
    public class Level : ScriptableObject
    {
        [SerializeField] private List<int> _requireExperiences = new();

        public IReadOnlyList<int> ExperienceQuntity => _requireExperiences;
    }
}
