using Ability.MeleeAbilities.Bloodlust;
using PlayerComponents;
using PlayerComponents.Controller;
using EnemyComponents;
using UnityEngine;
using Weapons.MeleeWeapon;

public class MeleePlayer : Player, IActivable
{
    [SerializeField] private Sword _sword;
    [SerializeField] private AnimatorState _animatorState;

    private float _movementVisualizationCoefficient = 0.2f;

    public bool IsActiveState { get; private set; }

    public float AttackSpeed { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (IsActiveState)
            {
                AddHealth(enemy.SetDamage());
            }
            else
            {
                LoseHealth(enemy.SetDamage());
            }

            if (Health.IsDead)
            {
                Destroy(gameObject);
            }
        }
    }

    public void UpgradeCharacteristikByBloodlust(Bloodlust bloodlust)
    {
        PlayerMovement.ChangeMoveSpeed(bloodlust.MovementSpeed);
        AttackSpeed += bloodlust.AttackSpeed;

        _animatorState.SetFloatValue(_animatorState.Speed, AttackSpeed);
        _animatorState.SetFloatValue(_animatorState.MovementSpeed, _movementVisualizationCoefficient);
        _movementVisualizationCoefficient += _movementVisualizationCoefficient;
    }

    public bool SetTrueActiveState() => IsActiveState = true;

    public bool SetFalseActiveState() => IsActiveState = false;
}
