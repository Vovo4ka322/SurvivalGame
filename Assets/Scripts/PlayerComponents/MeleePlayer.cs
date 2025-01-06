using Ability.MeleeAbilities.Bloodlust;
using Enemies;
using PlayerComponents;
using PlayerComponents.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player, IActivable//доделать разделение player на два класса
{
    [SerializeField] private PlayerMovement _playerMovement;

    public event Action<float> HealthChanged;

    public bool IsActiveState { get; private set; }

    public float AttackSpeed { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (IsActiveState)
            {
                Health.Add(enemy.SetDamage());
                HealthChanged?.Invoke(Health.Value);
            }
            else
            {
                Health.Lose(enemy.SetDamage());
                HealthChanged?.Invoke(Health.Value);
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
