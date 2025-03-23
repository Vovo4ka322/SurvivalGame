using Game.Scripts.EnemyComponents.EnemySettings;
using UnityEngine;

namespace Complexity
{
    [CreateAssetMenu(fileName = "Level", menuName = "DifficultyLevel")]
    public class DifficultyLevel : ScriptableObject
    {
        [field: SerializeField] public EnemyData EasyEnemyMeleeSkeleton {  get; private set; }

        [field: SerializeField] public EnemyData EasyEnemyRangeGost {  get; private set; }

        [field: SerializeField] public EnemyData MediumEnemyMeleeDemon {  get; private set; }

        [field: SerializeField] public EnemyData MediumEnemyMeleeSkeleton {  get; private set; }

        [field: SerializeField] public EnemyData HardEnemyMeleeGolem {  get; private set; }

        [field: SerializeField] public EnemyData HardEnemyMeleeSkeleton {  get; private set; }

        [field: SerializeField] public EnemyData BossLevel1 {  get; private set; }
    }
}