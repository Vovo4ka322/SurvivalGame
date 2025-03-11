using UnityEngine;

namespace Game.Scripts.MusicComponents.EffectSounds
{
    public class SoundCollection : MonoBehaviour
    {
        [SerializeField] private EnemyMeleeSoundEffects _meleeSoundEffects;
        [SerializeField] private EnemyRangedSoundEffects _rangedSoundEffects;
        [SerializeField] private EnemyHybridSoundEffects _hybridSoundEffects;
        [SerializeField] private BossSoundEffects _bossSoundEffects;
        [SerializeField] private MeleePlayerSoundEffects _meleePlayerSoundEffects;
        [SerializeField] private RangedPlayerSoundEffects _rangedPlayerSoundEffects;

        public EnemyMeleeSoundEffects MeleeSoundEffects => _meleeSoundEffects;
        public EnemyRangedSoundEffects RangedSoundEffects => _rangedSoundEffects;
        public EnemyHybridSoundEffects HybridSoundEffects => _hybridSoundEffects;
        public BossSoundEffects BossSoundEffects => _bossSoundEffects;
        public MeleePlayerSoundEffects MeleePlayerSoundEffects => _meleePlayerSoundEffects;
        public RangedPlayerSoundEffects RangedPlayerSoundEffects => _rangedPlayerSoundEffects;
    }
}