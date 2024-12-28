using System.Collections.Generic;
using UnityEngine;
namespace PlayerComponents
{
    [CreateAssetMenu(fileName = "PlayerLevel")]
    public class Level : ScriptableObject
    {
        [SerializeField] private List<int> _requireExperiences = new();

        public IReadOnlyList<int> ExperienceQunttity => _requireExperiences;
    }
}
