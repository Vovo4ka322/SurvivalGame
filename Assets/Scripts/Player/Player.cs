using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private CharacterType _characterType;
    [SerializeField] private MeleeAbilityUser _meleeAbility;
    [SerializeField] private BuffHolder _buffHolder;

    [SerializeField] private Buff _buff;//временно. Потом перенести в магазин

    public int Power { get; private set; }

    public int Armor { get; private set; }

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
            //_meleeAbility.ActivateBorrowedTime();

            Health.Lose(enemy.SetDamage());

            if (Health.IsDead)
            {
                Destroy(gameObject);
            }
        }
    }
}
