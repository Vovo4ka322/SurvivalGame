using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.MenuComponents;

namespace Game.Scripts.AbilityComponents
{
    public abstract class AbilityViewerBase : MonoBehaviour
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
        
        private IconUtility _iconUtility;

        public Image FirstAbility => _firstAbility;
        public Image SecondAbility => _secondAbility;
        public Image ThirdAbility => _thirdAbility;
        public Image FirstAbilityCooldown => _firstAbilityCooldown;
        public Image SecondAbilityCooldown => _secondAbilityCooldown;
        public List<Image> FirstAbilityImprovements => _firstAbilityImprovements;
        public List<Image> SecondAbilityImprovements => _secondAbilityImprovements;
        public List<Image> ThirdAbilityImprovements => _thirdAbilityImprovements;
        
        protected virtual void Awake()
        {
            _iconUtility = new IconUtility();
        }
        
        protected void SetInitialIconsDimmed()
        {
            _iconUtility.SetIconDimmed(_firstAbility, true);
            _iconUtility.SetIconDimmed(_secondAbility, true);
            _iconUtility.SetIconDimmed(_thirdAbility, true);
        }
        
        protected void Change(Image image, float cooldown, float value)
        {
            image.fillAmount = Mathf.InverseLerp(0, cooldown, value);
        }
        
        protected void Upgrade(List<Image> images, ref int improvementCounter, int maxValue, Image abilityIcon)
        {
            if(improvementCounter == maxValue)
            {
                return;
            }
            
            images[improvementCounter].gameObject.SetActive(true);
            
            improvementCounter++;
            
            if (improvementCounter == 1)
            {
                _iconUtility.SetIconDimmed(abilityIcon, false);
            }
        }
    }
}