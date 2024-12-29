using Ability.ArcherAbilities;
using Ability.MeleeAbilities;
using UnityEngine;

namespace Ability
{
    public class AbilityInput : MonoBehaviour//c������ ���, ����� ������������� � ����������� ��������� �������� ���
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private MeleeAbilityUser _meleeAbilityUser;
        [SerializeField] private ArcherAbilityUser _archerAbilityUser;
        //���� ��� �������

        private IAbilityUser _abilityUser;

        private void Awake()
        {
            //_abilityUser = _meleeAbilityUser.Init();
            _abilityUser = _archerAbilityUser.Init();
        }

        private void Update()
        {
            if(_playerInput.Player.UseFirstAbility.triggered)//��������� ������� ������� �� �������
                _abilityUser.UseFirstAbility();
            else if(_playerInput.Player.UseSecondAbility.triggered)
                _abilityUser.UseSecondAbility();
        }
    }
}
