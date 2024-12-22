using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private Pool<Arrow> _pool;
    [SerializeField] private ArrowData _arrowData;

    public Arrow Spawn(Transform transform)
    {
        Arrow arrow = _pool.Get(transform);
        arrow.gameObject.SetActive(true);
        arrow.Init(_arrowData.ArrowFlightSpeed, _arrowData.AttackRadius, _pool);

        return arrow;
    }

    public void PoolReturn(Arrow arrow)
    {
        _pool.Release(arrow);
    }
}
