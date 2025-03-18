using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.BuffComponents;

namespace Game.Scripts.MenuComponents.ShopComponents.Viewers
{
    public class BuffImprovementViewer : MonoBehaviour
    {
        [SerializeField] private BuffShop _buffShop;
        [SerializeField] private List<Image> _healthBuffUpgrades;
        [SerializeField] private List<Image> _armorBuffUpgrades;
        [SerializeField] private List<Image> _damageBuffUpgrades;
        [SerializeField] private List<Image> _attackSpeedBuffUpgrades;
        [SerializeField] private List<Image> _movementSpeedBuffUpgrades;

        private PlayerCharacteristicData _calculationFinalValue;

        private void OnEnable()
        {
            _buffShop.HealthUpgraded += OnHealthBuffUpgraded;
            _buffShop.ArmorUpgraded += OnArmorBuffUpgraded;
            _buffShop.DamageUpgraded += OnDamageBuffUpgraded;
            _buffShop.AttackSpeedUpgraded += OnAttackSpeedBuffUpgraded;
            _buffShop.MovementSpeedUpgraded += OnMovementSpeedBuffUpgraded;
        }

        private void OnDisable()
        {
            _buffShop.HealthUpgraded -= OnHealthBuffUpgraded;
            _buffShop.ArmorUpgraded -= OnArmorBuffUpgraded;
            _buffShop.DamageUpgraded -= OnDamageBuffUpgraded;
            _buffShop.AttackSpeedUpgraded -= OnAttackSpeedBuffUpgraded;
            _buffShop.MovementSpeedUpgraded -= OnMovementSpeedBuffUpgraded;
        }
        
        public void Init(PlayerCharacteristicData calculationFinalValue)
        {
            _calculationFinalValue = calculationFinalValue;

            UpdateValue(_healthBuffUpgrades, _calculationFinalValue.HealthLevelImprovment);
            UpdateValue(_armorBuffUpgrades, _calculationFinalValue.ArmorLevelImprovment);
            UpdateValue(_damageBuffUpgrades, _calculationFinalValue.DamageLevelImprovment);
            UpdateValue(_attackSpeedBuffUpgrades, _calculationFinalValue.AttackSpeedLevelImprovment);
            UpdateValue(_movementSpeedBuffUpgrades, _calculationFinalValue.MovementSpeedLevelImprovment);
        }
        
        private void UpdateValue(List<Image> image, int value)
        {
            for(int i = 0; i < value; i++)
            {
                Upgrade(image, i);
            }
        }
        
        private void OnHealthBuffUpgraded()
        {
            if(IsFull(_calculationFinalValue.HealthLevelImprovment))
            {
                return;
            }
            
            Upgrade(_healthBuffUpgrades, _calculationFinalValue.HealthLevelImprovment);
        }
        
        private void OnArmorBuffUpgraded()
        {
            if(IsFull(_calculationFinalValue.ArmorLevelImprovment))
            {
                return;
            }
            
            Upgrade(_armorBuffUpgrades, _calculationFinalValue.ArmorLevelImprovment);
        }
        
        private void OnDamageBuffUpgraded()
        {
            if(IsFull(_calculationFinalValue.DamageLevelImprovment))
            {
                return;
            }
            
            Upgrade(_damageBuffUpgrades, _calculationFinalValue.DamageLevelImprovment);
        }
        
        private void OnAttackSpeedBuffUpgraded()
        {
            if(IsFull(_calculationFinalValue.AttackSpeedLevelImprovment))
                return;

            Upgrade(_attackSpeedBuffUpgrades, _calculationFinalValue.AttackSpeedLevelImprovment);
        }

        private void OnMovementSpeedBuffUpgraded()
        {
            if(IsFull(_calculationFinalValue.MovementSpeedLevelImprovment))
                return;

            Upgrade(_movementSpeedBuffUpgrades, _calculationFinalValue.MovementSpeedLevelImprovment);
        }

        private bool IsFull(int value) => _buffShop.MaxCount <= value;

        private void Upgrade(List<Image> images, int index)
        {
            images[index].gameObject.SetActive(true);
        }
    }
}