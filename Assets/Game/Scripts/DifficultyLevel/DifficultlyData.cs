using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.DifficultyLevel
{
    public class DifficultlyData : MonoBehaviour
    {
        [field: SerializeField] public DifficultyLevel EasyLevel { get; private set; }
        [field: SerializeField] public DifficultyLevel MediumLevel { get; private set; }
        [field: SerializeField] public DifficultyLevel HardLevel { get; private set; }
    }

    public class DifficultySetter : MonoBehaviour
    {
        [SerializeField] private Button _easyEnemyButton;
        [SerializeField] private Button _mediumEnemyButton;
        [SerializeField] private Button _hardEnemyButton;

        private DifficultlyData _difficultlyData;

        private DifficultyLevel SetEasyEnemy()
        {
            return _difficultlyData.EasyLevel;
        }
    }

    public enum Difficults
    {
        Easy, Medium, Hard
    }
}