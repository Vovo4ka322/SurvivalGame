using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : Character, IHealable
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private CharacterType _characterType;
    [SerializeField] private BuffHolder _buffHolder;
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private Buff _buff;//временно. Потом перенести в магазин

    public float AttackSpeed { get; private set; }

    public int Power { get; private set; }

    public int Armor { get; private set; }

    public bool IsHealState { get; private set; }

    private void Awake()
    {
        _buffHolder.Add(_buff);// покупка бафов через магазин
        Init(_characterType);
    }

    public void Init(ICharacteristicable characteristicable)
    {
        float health = characteristicable.MaxHealth;
        Power = characteristicable.Power;
        Armor = characteristicable.Armor;

        foreach (var buff in _buffHolder.Baffs)
        {
            Power += buff.Power;
            Armor += buff.Armor;
            health += buff.MaxHealth;
        }

        Health.InitMaxValue(health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (IsHealState)
            {
                Health.Add(enemy.SetDamage());//потом сделать условие для бойца дальнего боя
            }
            else
            {
                Health.Lose(enemy.SetDamage());
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

    public void SetState(bool state)
    {
        IsHealState = state;
    }
}

public interface IHealable
{
    public bool IsHealState { get; }

    public void SetState(bool state);
}