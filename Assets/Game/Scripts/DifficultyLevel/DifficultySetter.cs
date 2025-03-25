using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.DifficultyLevel
{
    public class DifficultySetter : MonoBehaviour
    {
        [SerializeField] private DifficultyData _difficultlyData;

        [field: SerializeField] public DifficultyLevel CurrentDifficultyLevel { get; private set; }

        public void Init()
        {
            int defaultValue = 1;
            int savedDifficulty = PlayerPrefs.GetInt("Difficulty", defaultValue);
            SetDifficult((Difficults)savedDifficulty);
        }

        public DifficultyLevel SetDifficult(Difficults difficults)
        {
            PlayerPrefs.SetInt("Difficulty", (int)difficults);
            CurrentDifficultyLevel = Set(difficults);
            return CurrentDifficultyLevel;
        }

        private DifficultyLevel Set(Difficults difficults)
        {
            switch (difficults)
            {
                case Difficults.Easy:
                    return _difficultlyData.EasyLevel;
                case Difficults.Medium:
                    return _difficultlyData.MediumLevel;
                case Difficults.Hard:
                    return _difficultlyData.HardLevel;
                default:
                    return _difficultlyData.MediumLevel;
            }
        }
    }
}