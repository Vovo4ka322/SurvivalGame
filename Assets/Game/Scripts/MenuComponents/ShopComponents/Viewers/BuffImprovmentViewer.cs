using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MenuComponents.ShopComponents.Viewers
{
    public class BuffImprovmentViewer : MonoBehaviour
    {
        [SerializeField] private BuffShop _buffShop;

        [SerializeField] private List<Image> _healthBuffUpgraders;
        [SerializeField] private List<Image> _armorBuffUpgraders;
        [SerializeField] private List<Image> _damageBuffUpgraders;
        [SerializeField] private List<Image> _attackSpeedBuffUpgraders;
        [SerializeField] private List<Image> _movementSpeedBuffUpgraders;

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

            UpdateValue(_healthBuffUpgraders, _calculationFinalValue.HealthLevelImprovment);
            UpdateValue(_armorBuffUpgraders, _calculationFinalValue.ArmorLevelImprovment);
            UpdateValue(_damageBuffUpgraders, _calculationFinalValue.DamageLevelImprovment);
            UpdateValue(_attackSpeedBuffUpgraders, _calculationFinalValue.AttackSpeedLevelImprovment);
            UpdateValue(_movementSpeedBuffUpgraders, _calculationFinalValue.MovementSpeedLevelImprovment);
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
            
            Upgrade(_healthBuffUpgraders, _calculationFinalValue.HealthLevelImprovment);
        }
        
        private void OnArmorBuffUpgraded()
        {
            if(IsFull(_calculationFinalValue.ArmorLevelImprovment))
            {
                return;
            }
            
            Upgrade(_armorBuffUpgraders, _calculationFinalValue.ArmorLevelImprovment);
        }
        
        private void OnDamageBuffUpgraded()
        {
            if(IsFull(_calculationFinalValue.DamageLevelImprovment))
            {
                return;
            }
            
            Upgrade(_damageBuffUpgraders, _calculationFinalValue.DamageLevelImprovment);
        }
        
        private void OnAttackSpeedBuffUpgraded()
        {
            if(IsFull(_calculationFinalValue.AttackSpeedLevelImprovment))
                return;

            Upgrade(_attackSpeedBuffUpgraders, _calculationFinalValue.AttackSpeedLevelImprovment);
        }

        private void OnMovementSpeedBuffUpgraded()
        {
            if(IsFull(_calculationFinalValue.MovementSpeedLevelImprovment))
                return;

            Upgrade(_movementSpeedBuffUpgraders, _calculationFinalValue.MovementSpeedLevelImprovment);
        }

        private bool IsFull(int value) => _buffShop.MaxCount == value;

        private void Upgrade(List<Image> images, int index)
        {
            images[index].gameObject.SetActive(true);
        }
    }
}