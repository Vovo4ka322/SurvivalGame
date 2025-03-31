using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuffComponents
{
    public class BuffImprovement : MonoBehaviour
    {
        [SerializeField] private List<BuffKeeper> _buffKeepersList;

        private Dictionary<int, BuffKeeper> _buffKeepers;
        
        private int _counterForHealthBuff;
        private int _counterForArmorBuff;
        private int _counterForDamageBuff;
        private int _counterForMovementSpeedBuff;
        private int _counterForAttackSpeedBuff;

        private readonly int _maxValue = 5;
        
        public Buff HealthBuff { get; private set; }
        public Buff ArmorBuff { get; private set; }
        public Buff DamageBuff { get; private set; }
        public Buff MovementSpeedBuff { get; private set; }
        public Buff AttackSpeedBuff { get; private set; }
        
        private void Awake()
        {
            _buffKeepers = new Dictionary<int, BuffKeeper>();
            
            foreach (BuffKeeper keeper in _buffKeepersList)
            {
                if (!_buffKeepers.ContainsKey(keeper.Level))
                {
                    _buffKeepers.Add(keeper.Level, keeper);
                }
            }
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
            HealthBuff = _buffKeepers[level].GetBuff(BuffType.Health);
        }

        private void InitArmor(int level)
        {
            ArmorBuff = _buffKeepers[level].GetBuff(BuffType.Armor);
        }

        private void InitDamage(int level)
        {
            DamageBuff = _buffKeepers[level].GetBuff(BuffType.Damage);
        }

        private void InitMovementSpeed(int level)
        {
            MovementSpeedBuff = _buffKeepers[level].GetBuff(BuffType.MovementSpeed);
        }

        private void InitAttackSpeed(int level)
        {
            AttackSpeedBuff = _buffKeepers[level].GetBuff(BuffType.AttackSpeed);
        }
    }
}