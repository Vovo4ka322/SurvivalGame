using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyMovement _movement;

    private EnemyData _data;
    private Transform _target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Weapon weapon))
        {
            Health.Lose(weapon.WeaponData.Damage);

            if (Health.IsDead)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        _movement.Move(_target, _data.MoveSpeed);
    }

    public void Init(EnemyData data, Transform target)
    {
        _target = target;
        _data = data;
        Health.InitMaxValue(data.MaxHealth);
    }

    public float SetDamage()
    {
        return _data.Damage;
    }
}
