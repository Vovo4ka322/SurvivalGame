using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuffComponents
{
    public class BuffImprovement : MonoBehaviour
    {
        [Header("BuffKeeperLevel")]
        [SerializeField] private BuffKeeper _buffKeeperFirstLevel;
        [SerializeField] private BuffKeeper _buffKeeperSecondLevel;
        [SerializeField] private BuffKeeper _buffKeeperThirdLevel;
        [SerializeField] private BuffKeeper _buffKeeperFourthLevel;
        [SerializeField] private BuffKeeper _buffKeeperFifthLevel;

        private readonly int _maxValue = 5;
        private readonly int _firstLevel = 1;
        private readonly int _secondLevel = 2;
        private readonly int _thirdLevel = 3;
        private readonly int _fourthLevel = 4;
        private readonly int _fifthLevel = 5;
        private readonly int _firstUpgrade = 0;
        private readonly int _secondUpgrade = 1;
        private readonly int _thirdUpgrade = 2;
        private readonly int _fourthUpgrade = 3;
        private readonly int _fifthUpgrade = 4;

        private Dictionary<int, BuffKeeper> _buffKeepers;
        
        private int _counterForHealthBuff;
        private int _counterForArmorBuff;
        private int _counterForDamageBuff;
        private int _counterForMovementSpeedBuff;
        private int _counterForAttackSpeedBuff;
        
        public HealthBuff HealthBuff { get; private set; }
        public ArmorBuff ArmorBuff { get; private set; }
        public DamageBuff DamageBuff { get; private set; }
        public MovementSpeedBuff MovementSpeedBuff { get; private set; }
        public AttackSpeedBuff AttackSpeedBuff { get; private set; }
        
        private void Awake()
        {
            _buffKeepers = new Dictionary<int, BuffKeeper>
            {
                { _firstLevel, _buffKeeperFirstLevel },
                { _secondLevel, _buffKeeperSecondLevel },
                { _thirdLevel, _buffKeeperThirdLevel },
                { _fourthLevel, _buffKeeperFourthLevel },
                { _fifthLevel, _buffKeeperFifthLevel },
            };
        }
        
        public void InitHealthLevel(int healthLevel) => _counterForHealthBuff = healthLevel;
        
        public void InitArmorLevel(int armorLevel) => _counterForArmorBuff = armorLevel;
        
        public void InitDamageLevel(int damageLevel) => _counterForDamageBuff = damageLevel;
        
        public void InitAttackSpeedLevel(int attackSpeedLevel) => _counterForAttackSpeedBuff = attackSpeedLevel;
        
        public void InitMovementSpeedLevel(int movementSpeedLevel) => _counterForMovementSpeedBuff = movementSpeedLevel;

        public void UpgradeHealth()
        {
            if(IsTrue(_counterForHealthBuff, _firstUpgrade))
                InitHealth(_firstLevel);
            else if(IsTrue(_counterForHealthBuff, _secondUpgrade))
                InitHealth(_secondLevel);
            else if(IsTrue(_counterForHealthBuff, _thirdUpgrade))
                InitHealth(_thirdLevel);
            else if(IsTrue(_counterForHealthBuff, _fourthUpgrade))
                InitHealth(_fourthLevel);
            else if(IsTrue(_counterForHealthBuff, _fifthUpgrade))
                InitHealth(_fifthLevel);
        }

        public void UpgradeArmor()
        {
            if(IsTrue(_counterForArmorBuff, _firstUpgrade))
                InitArmor(_firstLevel);
            else if(IsTrue(_counterForArmorBuff, _secondUpgrade))
                InitArmor(_secondLevel);
            else if(IsTrue(_counterForArmorBuff, _thirdUpgrade))
                InitArmor(_thirdLevel);
            else if(IsTrue(_counterForArmorBuff, _fourthUpgrade))
                InitArmor(_fourthLevel);
            else if(IsTrue(_counterForArmorBuff, _fifthUpgrade))
                InitArmor(_fifthLevel);
        }

        public void UpgradeDamage()
        {
            if(IsTrue(_counterForDamageBuff, _firstUpgrade))
                InitDamage(_firstLevel);
            else if(IsTrue(_counterForDamageBuff, _secondUpgrade))
                InitDamage(_secondLevel);
            else if(IsTrue(_counterForDamageBuff, _thirdUpgrade))
                InitDamage(_thirdLevel);
            else if(IsTrue(_counterForDamageBuff, _fourthUpgrade))
                InitDamage(_fourthLevel);
            else if(IsTrue(_counterForDamageBuff, _fifthUpgrade))
                InitDamage(_fifthLevel);
        }

        public void UpgradeAttackSpeed()
        {
            if(IsTrue(_counterForAttackSpeedBuff, _firstUpgrade))
                InitAttackSpeed(_firstLevel);
            else if(IsTrue(_counterForAttackSpeedBuff, _secondUpgrade))
                InitAttackSpeed(_secondLevel);
            else if(IsTrue(_counterForAttackSpeedBuff, _thirdUpgrade))
                InitAttackSpeed(_thirdLevel);
            else if(IsTrue(_counterForAttackSpeedBuff, _fourthUpgrade))
                InitAttackSpeed(_fourthLevel);
            else if(IsTrue(_counterForAttackSpeedBuff, _fifthUpgrade))
                InitAttackSpeed(_fifthLevel);
        }

        public void UpgradeMovementSpeed()
        {
            if(IsTrue(_counterForMovementSpeedBuff, _firstUpgrade))
                InitMovementSpeed(_firstLevel);
            else if(IsTrue(_counterForMovementSpeedBuff, _secondUpgrade))
                InitMovementSpeed(_secondLevel);
            else if(IsTrue(_counterForMovementSpeedBuff, _thirdUpgrade))
                InitMovementSpeed(_thirdLevel);
            else if(IsTrue(_counterForMovementSpeedBuff, _fourthUpgrade))
                InitMovementSpeed(_fourthLevel);
            else if(IsTrue(_counterForMovementSpeedBuff, _fifthUpgrade))
                InitMovementSpeed(_fifthLevel);
        }

        private void InitArmor(int level)
        {
            if(IsMaxValue(_counterForArmorBuff))
            {
                return;
            }

            ArmorBuff = _buffKeepers[level].ArmorBuffScriptableObject;
            
            _counterForArmorBuff++;
        }

        private void InitDamage(int level)
        {
            if(IsMaxValue(_counterForDamageBuff))
            {
                return;
            }

            DamageBuff = _buffKeepers[level].DamageBuffScriptableObject;
            
            _counterForDamageBuff++;
        }

        private void InitMovementSpeed(int level)
        {
            if(IsMaxValue(_counterForMovementSpeedBuff))
            {
                return;
            }

            MovementSpeedBuff = _buffKeepers[level].MovementSpeedBuffScriptableObject;
            
            _counterForMovementSpeedBuff++;
        }

        private void InitAttackSpeed(int level)
        {
            if(IsMaxValue(_counterForAttackSpeedBuff))
            {
                return;
            }

            AttackSpeedBuff = _buffKeepers[level].AttackSpeedBuffScriptableObject;
            
            _counterForAttackSpeedBuff++;
        }

        private void InitHealth(int level)
        {
            if(IsMaxValue(_counterForHealthBuff))
            {
                return;
            }

            HealthBuff = _buffKeepers[level].HealthBuffScriptableObject;
            
            _counterForHealthBuff++;
        }

        private bool IsMaxValue(int value) => value == _maxValue;

        private bool IsTrue(int counter, int numberOfUpgrade) => counter == numberOfUpgrade;
    }
}