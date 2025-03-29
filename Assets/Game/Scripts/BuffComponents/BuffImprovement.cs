using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.BuffComponents.BuffVariants;

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

        private Dictionary<int, BuffKeeper> _buffKeepers;

        private int _counterForHealthBuff;
        private int _counterForArmorBuff;
        private int _counterForDamageBuff;
        private int _counterForMovementSpeedBuff;
        private int _counterForAttackSpeedBuff;

        private readonly int _maxValue = 5;
        
        private readonly int _firstLevel = 1;
        private readonly int _secondLevel = 2;
        private readonly int _thirdLevel = 3;
        private readonly int _fourthLevel = 4;
        private readonly int _fifthLevel = 5;

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
        
        public void UpgradeHealth() => UpgradeBuff(ref _counterForHealthBuff, InitHealth);
        
        public void UpgradeArmor() => UpgradeBuff(ref _counterForArmorBuff, InitArmor);
        
        public void UpgradeDamage() => UpgradeBuff(ref _counterForDamageBuff, InitDamage);
        
        public void UpgradeAttackSpeed() => UpgradeBuff(ref _counterForAttackSpeedBuff, InitAttackSpeed);
        
        public void UpgradeMovementSpeed() => UpgradeBuff(ref _counterForMovementSpeedBuff, InitMovementSpeed);
        
        private void UpgradeBuff(ref int counter, Action<int> initAction)
        {
            if (counter >= _maxValue)
                return;

            int level = counter + 1;
            
            initAction(level);
            
            counter++;
        }
        
        private void InitHealth(int level)
        {
            HealthBuff = _buffKeepers[level].HealthBuffScriptableObject;
        }

        private void InitArmor(int level)
        {
            ArmorBuff = _buffKeepers[level].ArmorBuffScriptableObject;
        }

        private void InitDamage(int level)
        {
            DamageBuff = _buffKeepers[level].DamageBuffScriptableObject;
        }

        private void InitMovementSpeed(int level)
        {
            MovementSpeedBuff = _buffKeepers[level].MovementSpeedBuffScriptableObject;
        }

        private void InitAttackSpeed(int level)
        {
            AttackSpeedBuff = _buffKeepers[level].AttackSpeedBuffScriptableObject;
        }
    }
}