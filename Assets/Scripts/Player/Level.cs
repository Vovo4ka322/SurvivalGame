using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerLevel")]
    public class Level : ScriptableObject
    {
        [SerializeField] private List<int> _requireExperiences = new();

        public IReadOnlyList<int> ExperienceQunttity => _requireExperiences;
    }
}
