using Abilities;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class AbilityInput : MonoBehaviour//cделать так, чтобы отслеживались и способности персонажа дальнего боя
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MeleeAbilityUser _meleeAbilityUser;
    //Поле для лучника

    private IAbilityUser _abilityUser;

    private void Awake()
    {
        _abilityUser = _meleeAbilityUser.Init();
    }

    private void Update()
    {
        if(_playerController.FirstAbilityKeyPressed)//дополнить условие кнопкой на канвасе
            _abilityUser.UseFirstAbility();
        else if(_playerController.SecondAbilityKeyPressed)
            _abilityUser.UseSecondAbility();
    }
}
