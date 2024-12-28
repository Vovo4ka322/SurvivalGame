using Ability.ArcherAbilities;
using Ability.MeleeAbilities;
using PlayerComponents.Controller;
using UnityEngine;

namespace Ability
{
    public class AbilityInput : MonoBehaviour//cделать так, чтобы отслеживались и способности персонажа дальнего боя
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private MeleeAbilityUser _meleeAbilityUser;
        [SerializeField] private ArcherAbilityUser _archerAbilityUser;
        //Поле для лучника

        private IAbilityUser _abilityUser;

        private void Awake()
        {
            //_abilityUser = _meleeAbilityUser.Init();
            _abilityUser = _archerAbilityUser.Init();
        }

        private void Update()
        {
            if(_playerController.FirstAbilityKeyPressed)//дополнить условие кнопкой на канвасе
                _abilityUser.UseFirstAbility();
            else if(_playerController.SecondAbilityKeyPressed)
                _abilityUser.UseSecondAbility();
        }
    }
}
