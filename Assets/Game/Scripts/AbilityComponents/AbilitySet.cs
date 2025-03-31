using UnityEngine;
using Game.Scripts.AbilityComponents.ArcherAbilities.BlurComponents;
using Game.Scripts.AbilityComponents.ArcherAbilities.InsatiableHungerComponents;
using Game.Scripts.AbilityComponents.ArcherAbilities.MultiShotComponents;
using Game.Scripts.AbilityComponents.MeleeAbilities.BladeFuryComponents;
using Game.Scripts.AbilityComponents.MeleeAbilities.BloodLustComponents;
using Game.Scripts.AbilityComponents.MeleeAbilities.BorrowedTimeComponents;

namespace Game.Scripts.AbilityComponents
{
    [CreateAssetMenu(fileName = "AbilitySet", menuName = "Ability/AbilityData")]
    public class AbilitySet : ScriptableObject
    {
        [Header("Melee Abilities")]
        [SerializeField] private BladeFury _bladeFury;
        [SerializeField] private BorrowedTime _borrowedTime;
        [SerializeField] private BloodLust _bloodLust;

        [Header("Range Abilities")]
        [SerializeField] private MultiShot _multiShot;
        [SerializeField] private InsatiableHunger _insatiableHunger;
        [SerializeField] private Blur _blur;

        public BladeFury BladeFury => _bladeFury;
        public BorrowedTime BorrowedTime => _borrowedTime;
        public BloodLust BloodLust => _bloodLust;
        public MultiShot MultiShot => _multiShot;
        public InsatiableHunger InsatiableHunger => _insatiableHunger;
        public Blur Blur => _blur;
    }
}