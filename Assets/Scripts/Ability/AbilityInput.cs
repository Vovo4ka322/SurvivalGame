using Abilities;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class AbilityInput : MonoBehaviour//c������ ���, ����� ������������� � ����������� ��������� �������� ���
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MeleeAbilityUser _meleeAbilityUser;
    //���� ��� �������

    private IAbilityUser _abilityUser;

    private void Awake()
    {
        _abilityUser = _meleeAbilityUser.Init();
    }

    private void Update()
    {
        if(_playerController.FirstAbilityKeyPressed)//��������� ������� ������� �� �������
            _abilityUser.UseFirstAbility();
        else if(_playerController.SecondAbilityKeyPressed)
            _abilityUser.UseSecondAbility();
    }
}
