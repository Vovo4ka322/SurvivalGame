using Ability.MeleeAbilities.Bloodlust;
using Enemies;
using PlayerComponents;
using PlayerComponents.Controller;
using System;
using UnityEngine;

public class MeleePlayer : Player, IActivable
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Sword _sword;

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
        _playerMovement.ChangeMoveSpeed(bloodlust.MovementSpeed);
        AttackSpeed += bloodlust.AttackSpeed;
    }

    public bool SetTrueActiveState() => IsActiveState = true;

    public bool SetFalseActiveState() => IsActiveState = false;
}
