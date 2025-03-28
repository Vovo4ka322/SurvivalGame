using UnityEngine;

namespace Game.Scripts.DifficultyLevel
{
    [CreateAssetMenu(fileName = "DifficultyData", menuName = "DifficultyData")]
    public class DifficultyData : ScriptableObject
    {
        [field: SerializeField] public GameDifficultyLevel EasyLevel { get; private set; }
        [field: SerializeField] public GameDifficultyLevel MediumLevel { get; private set; }
        [field: SerializeField] public GameDifficultyLevel HardLevel { get; private set; }
    }
}