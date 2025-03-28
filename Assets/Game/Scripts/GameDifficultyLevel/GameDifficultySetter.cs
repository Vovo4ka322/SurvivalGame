using UnityEngine;

namespace Game.Scripts.GameDifficultyLevel
{
    public class GameDifficultySetter : MonoBehaviour
    {
        [SerializeField] private DifficultyData _difficultlyData;

        [field: SerializeField] public GameDifficultyLevel CurrentDifficultyLevel { get; private set; }

        public void Init()
        {
            int defaultValue = 1;
            int savedDifficulty = PlayerPrefs.GetInt("Difficulty", defaultValue);
            
            SetDifficult((Difficults)savedDifficulty);
        }

        public GameDifficultyLevel SetDifficult(Difficults difficults)
        {
            PlayerPrefs.SetInt("Difficulty", (int)difficults);
            
            CurrentDifficultyLevel = Set(difficults);
            
            return CurrentDifficultyLevel;
        }

        private GameDifficultyLevel Set(Difficults difficults)
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