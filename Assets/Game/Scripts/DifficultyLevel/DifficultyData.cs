using UnityEngine;

namespace Game.Scripts.DifficultyLevel
{
    [CreateAssetMenu(fileName = "DifficultyData", menuName = "DifficultyData")]
    public class DifficultyData : ScriptableObject
    {
        [field: SerializeField] public DifficultyLevel EasyLevel { get; private set; }
        [field: SerializeField] public DifficultyLevel MediumLevel { get; private set; }
        [field: SerializeField] public DifficultyLevel HardLevel { get; private set; }
    }
}