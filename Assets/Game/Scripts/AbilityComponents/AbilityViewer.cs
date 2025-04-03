using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.AbilityComponents.ArcherAbilities;
using Game.Scripts.AbilityComponents.MeleeAbilities;
using Game.Scripts.MenuComponents;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents
{
    public class AbilityViewer : MonoBehaviour
    {
        [Header("Ability Images")] 
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
        private CharacterType _characterType;
        private MeleePlayerAbility _meleeAbility;
        private RangePlayerAbility _rangeAbility;

        private int _firstImprovement = 0;
        private int _secondImprovement = 0;
        private int _thirdImprovement = 0;

        private void Awake()
        {
            _iconUtility = new IconUtility();
        }

        public void Init(Player player)
        {
            _meleeAbility = player.GetComponentInChildren<MeleePlayerAbility>();

            if (_meleeAbility != null)
            {
                _characterType = CharacterType.Melee;
                SetInitialIconsDimmed();
                SubscribeMeleeEvents();

                return;
            }

            _rangeAbility = player.GetComponentInChildren<RangePlayerAbility>();

            if (_rangeAbility != null)
            {
                _characterType = CharacterType.Range;
                SetInitialIconsDimmed();
                SubscribeRangeEvents();

                return;
            }
        }

        private void OnDisable()
        {
            if (_characterType == CharacterType.Melee)
            {
                _meleeAbility.BladeFury.Used -= OnFirstChanged;
                _meleeAbility.BorrowedTime.Used -= OnSecondChanged;
                _meleeAbility.BladeFuryUpgraded -= OnFirstUpgraded;
                _meleeAbility.BorrowedTimeIUpgraded -= OnSecondUpgraded;
                _meleeAbility.BloodLustIUpgraded -= OnThirdUpgraded;
            }
            else if (_characterType == CharacterType.Range)
            {
                _rangeAbility.MultiShotUser.Used -= OnFirstChanged;
                _rangeAbility.InsatiableHunger.Used -= OnSecondChanged;
                _rangeAbility.MultiShotUpgraded -= OnFirstUpgraded;
                _rangeAbility.InsatiableHungerUpgraded -= OnSecondUpgraded;
                _rangeAbility.BlurUpgraded -= OnThirdUpgraded;
            }
        }

        private void SetInitialIconsDimmed()
        {
            _iconUtility.SetIconDimmed(_firstAbility, true);
            _iconUtility.SetIconDimmed(_secondAbility, true);
            _iconUtility.SetIconDimmed(_thirdAbility, true);
        }

        private void Change(Image image, float cooldown, float value)
        {
            image.fillAmount = Mathf.InverseLerp(0, cooldown, value);
        }

        private void Upgrade(List<Image> images, ref int improvementCounter, int maxValue, Image abilityIcon)
        {
            if (improvementCounter == maxValue)
                return;

            images[improvementCounter].gameObject.SetActive(true);

            improvementCounter++;

            if (improvementCounter == 1)
                _iconUtility.SetIconDimmed(abilityIcon, false);
        }

        private void SubscribeMeleeEvents()
        {
            _meleeAbility.BladeFury.Used += OnFirstChanged;
            _meleeAbility.BorrowedTime.Used += OnSecondChanged;
            _meleeAbility.BladeFuryUpgraded += OnFirstUpgraded;
            _meleeAbility.BorrowedTimeIUpgraded += OnSecondUpgraded;
            _meleeAbility.BloodLustIUpgraded += OnThirdUpgraded;
        }

        private void SubscribeRangeEvents()
        {
            _rangeAbility.MultiShotUser.Used += OnFirstChanged;
            _rangeAbility.InsatiableHunger.Used += OnSecondChanged;
            _rangeAbility.MultiShotUpgraded += OnFirstUpgraded;
            _rangeAbility.InsatiableHungerUpgraded += OnSecondUpgraded;
            _rangeAbility.BlurUpgraded += OnThirdUpgraded;
        }

        private void OnFirstChanged(float value)
        {
            if (_characterType == CharacterType.Melee)
            {
                Change(_firstAbilityCooldown, _meleeAbility.BladeFury.BladeFury.CooldownTime, value);
            }
            else if (_characterType == CharacterType.Range)
            {
                Change(_firstAbilityCooldown, _rangeAbility.MultiShotUser.MultiShot.CooldownTime, value);
            }
        }

        private void OnSecondChanged(float value)
        {
            if (_characterType == CharacterType.Melee)
            {
                Change(_secondAbilityCooldown, _meleeAbility.BorrowedTime.BorrowedTime.CooldownTime, value);
            }
            else if (_characterType == CharacterType.Range)
            {
                Change(_secondAbilityCooldown, _rangeAbility.InsatiableHunger.InsatiableHunger.CooldownTime, value);
            }
        }

        private void OnFirstUpgraded()
        {
            if (_characterType == CharacterType.Melee)
            {
                Upgrade(_firstAbilityImprovements, ref _firstImprovement, _meleeAbility.MaxValue, _firstAbility);
            }
            else if (_characterType == CharacterType.Range)
            {
                Upgrade(_firstAbilityImprovements, ref _firstImprovement, _rangeAbility.MaxValue, _firstAbility);
            }
        }

        private void OnSecondUpgraded()
        {
            if (_characterType == CharacterType.Melee)
            {
                Upgrade(_secondAbilityImprovements, ref _secondImprovement, _meleeAbility.MaxValue, _secondAbility);
            }
            else if (_characterType == CharacterType.Range)
            {
                Upgrade(_secondAbilityImprovements, ref _secondImprovement, _rangeAbility.MaxValue, _secondAbility);
            }
        }

        private void OnThirdUpgraded()
        {
            if (_characterType == CharacterType.Melee)
            {
                Upgrade(_thirdAbilityImprovements, ref _thirdImprovement, _meleeAbility.MaxValue, _thirdAbility);
            }
            else if (_characterType == CharacterType.Range)
            {
                Upgrade(_thirdAbilityImprovements, ref _thirdImprovement, _rangeAbility.MaxValue, _thirdAbility);
            }
        }
    }
}