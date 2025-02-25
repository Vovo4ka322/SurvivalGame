using Ability.ArcherAbilities;
using UnityEngine;
using UnityEngine.UI;

public class RangeAbilityInput : MonoBehaviour
{
    [SerializeField] private ArcherAbilityUser _rangeAbilityUser;
    [SerializeField] private PlayerController _playerController;

    private Button _firstAbilityUse;
    private Button _secondAbilityUse;
    private Button _firstUpgradeButton;
    private Button _secondUpgradeButton;
    private Button _thirdUpgradeButton;

    private void Start()
    {
        _firstAbilityUse.onClick.AddListener(_rangeAbilityUser.UseFirstAbility);
        _secondAbilityUse.onClick.AddListener(_rangeAbilityUser.UseSecondAbility);
        _firstUpgradeButton.onClick.AddListener(_rangeAbilityUser.UpgradeFirstAbility);
        _secondUpgradeButton.onClick.AddListener(_rangeAbilityUser.UpgradeSecondAbility);
        _thirdUpgradeButton.onClick.AddListener(_rangeAbilityUser.UpgradeThirdAbility);
    }

    private void Update()
    {
        if (_playerController.FirstAbilityKeyPressed)
            _rangeAbilityUser.UseFirstAbility();
        else if (_playerController.SecondAbilityKeyPressed)
            _rangeAbilityUser.UseSecondAbility();
    }

    private void OnDisable()
    {
        _firstAbilityUse.onClick.RemoveListener(_rangeAbilityUser.UseFirstAbility);
        _secondAbilityUse.onClick.RemoveListener(_rangeAbilityUser.UseSecondAbility);
        _firstUpgradeButton.onClick.RemoveListener(_rangeAbilityUser.UpgradeFirstAbility);
        _secondUpgradeButton.onClick.RemoveListener(_rangeAbilityUser.UpgradeSecondAbility);
        _thirdUpgradeButton.onClick.RemoveListener(_rangeAbilityUser.UpgradeThirdAbility);
    }

    public void Init(Button firstAbilityUse, Button secondAbilityUse, Button firstUpgradeButton, Button secondUpgradeButton, Button thirdUpgradeButton)
    {
        _firstAbilityUse = firstAbilityUse;
        _secondAbilityUse = secondAbilityUse;
        _firstUpgradeButton = firstUpgradeButton;
        _secondUpgradeButton = secondUpgradeButton;
        _thirdUpgradeButton = thirdUpgradeButton;
    }
}