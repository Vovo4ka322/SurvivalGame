using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyMovement _movement;

    private EnemyData _data;
    private Transform _target;

    private void Update()
    {
        _movement.Move(_target, _data.MoveSpeed);
    }

    public void Init(EnemyData data, Transform target)
    {
        _target = target;
        _data = data;
        Health.InitValue(data.MaxHealth);
    }

    public void SetDamage(Enemy enemy)//наношу кому-то урон
    {
        enemy.Health.Lose(_data.Damage);
    }
}
