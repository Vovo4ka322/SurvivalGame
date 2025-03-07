using System;
using System.Collections;
using UnityEngine;
using Game.Scripts.EnemyComponents;
using Game.Scripts.Interfaces;
using Weapons;

public class Arrow : MonoBehaviour
{
    private Vector3 _direction;
    private IPoolReciver<Arrow> _poolReciver;
    private Coroutine _coroutine;
    private float _speedFlight;
    private float _radius;

    public event Action Touched;

    [field: SerializeField] public Weapon Weapon {  get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy _))
            Touched?.Invoke();
    }

    public void StartFly(Vector3 direction, Vector3 position)
    {
        transform.position = position;
        _direction = direction.normalized;
        transform.forward = _direction;

        _coroutine = StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        float distanceTravelled = 0;

        while (distanceTravelled < _radius)
        {
            float step = _speedFlight * Time.deltaTime;
            transform.position += _direction * step;
            distanceTravelled += step;

            yield return null;
        }

        _poolReciver.Release(this);
    }

    public void Init(float speedFlight, float radius, IPoolReciver<Arrow> arrowPool)
    {
        _speedFlight = speedFlight;
        _radius = radius;
        _poolReciver = arrowPool;
    }
}