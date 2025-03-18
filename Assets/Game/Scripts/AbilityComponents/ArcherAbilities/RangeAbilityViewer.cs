using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.MenuComponents;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.ArcherAbilities
{
    public class RangeAbilityViewer : MonoBehaviour
    {
        [Header("Images Abilities")]
        [SerializeField] private Image _firstAbility;
        [SerializeField] private Image _secondAbility;
        [SerializeField] private Image _thirdAbility;
        
        [Header("Cooldown Images")]
        [SerializeField] private Image _firstAbilityCooldown;
        [SerializeField] private Image _secondAbilityCooldown;

        [Header("Upgrade Images")]
        [SerializeField] private List<Image> _firstAbilityImprovements;
        [SerializeField] private List<Image> _secondAbilityImprovements;
        [SerializeField] private List<Image> _thirdAbilityImprovements;
        
        private ArcherAbilityUser _archerAbilityUser;
        private IconUtility _iconUtility;
        
        private int _multiShotImprovement = 0;
        private int _insatiableHungerImprovement = 0;
        private int _blurImprovement = 0;

        private void OnDisable()
        {
            _archerAbilityUser.MultiShotUser.Used -= OnMultiShotChanged;
            _archerAbilityUser.InsatiableHunger.Used -= OnInsatiableHungerChanged;
            _archerAbilityUser.MultiShotUpgraded -= OnMultiShotUpgraded;
            _archerAbilityUser.InsatiableHungerUpgraded -= OnInsatiableHungerUpgraded;
            _archerAbilityUser.BlurUpgraded -= OnBlurUpgraded;
        }

        public void Init(Player player)
        {
            _archerAbilityUser = player.GetComponentInChildren<ArcherAbilityUser>();
            _iconUtility = new IconUtility();
            
            SubscribeToEvents();
            
            _iconUtility.SetIconDimmed(_firstAbility, true);
            _iconUtility.SetIconDimmed(_secondAbility, true);
            _iconUtility.SetIconDimmed(_thirdAbility, true);
        }

        private void SubscribeToEvents()
        {
            _archerAbilityUser.MultiShotUser.Used += OnMultiShotChanged;
            _archerAbilityUser.InsatiableHunger.Used += OnInsatiableHungerChanged;
            _archerAbilityUser.MultiShotUpgraded += OnMultiShotUpgraded;
            _archerAbilityUser.InsatiableHungerUpgraded += OnInsatiableHungerUpgraded;
            _archerAbilityUser.BlurUpgraded += OnBlurUpgraded;
        }

        private void OnMultiShotChanged(float value)
        {
            Change(_firstAbilityCooldown, _archerAbilityUser.MultiShotUser.MultiShot.CooldownTime, value);
        }

        private void OnInsatiableHungerChanged(float value)
        {
            Change(_secondAbilityCooldown, _archerAbilityUser.InsatiableHunger.InsatiableHunger.CooldownTime, value);
        }

        private void Change(Image image, float cooldown, float value)
        {
            image.fillAmount = Mathf.InverseLerp(0, cooldown, value);
        }

        private void Upgrade(List<Image> images, int index)
        {
            images[index].gameObject.SetActive(true);
        }

        private void OnMultiShotUpgraded()
        {
            if(_multiShotImprovement == _archerAbilityUser.MaxValue)
                return;

            Upgrade(_firstAbilityImprovements, _multiShotImprovement);
            _multiShotImprovement++;

            if(_multiShotImprovement == 1)
            {
                _iconUtility.SetIconDimmed(_firstAbility, false);
            }
        }

        private void OnInsatiableHungerUpgraded()
        {
            if(_insatiableHungerImprovement == _archerAbilityUser.MaxValue)
                return;

            Upgrade(_secondAbilityImprovements, _insatiableHungerImprovement);
            _insatiableHungerImprovement++;
            
            if(_insatiableHungerImprovement == 1)
            {
                _iconUtility.SetIconDimmed(_secondAbility, false);
            }
        }

        private void OnBlurUpgraded()
        {
            if(_blurImprovement == _archerAbilityUser.MaxValue)
                return;

            Upgrade(_thirdAbilityImprovements, _blurImprovement);
            _blurImprovement++;
            
            if(_blurImprovement == 1)
            {
                _iconUtility.SetIconDimmed(_thirdAbility, false);
            }
        }
    }
}