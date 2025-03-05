using System;
using UnityEngine;
using Game.Scripts.EnemyComponents;
using Game.Scripts.PoolComponents;
using Game.Scripts.ProjectileComponents;
using Game.Scripts.ProjectileComponents.ProjectileInterfaces;

public class ArrowProjectile : BaseProjectile
{
    private readonly IProjectileMovement _movement = new ArrowMovement();
    
    public event Action Touched;
    
    public override void Launch(Vector3 targetPosition, ProjectilePool<BaseProjectile> pool, IExplosionHandler explosionHandler)
    {
        Pool = pool;
        
        InitializeProjectile(_movement, pool, explosionHandler, ConfiguredLifetime);
        LaunchProjectile(targetPosition);
    }

    public void Live()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy _))
        {
            Touched?.Invoke();
        }
    }
    /*private Vector3 _direction;
    private IPoolReciver<ArrowProjectile> _poolReciver;
    private Coroutine _coroutine;
    private float _speedFlight;
    private float _radius;

    public event Action Touched;

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

    public void Init(float speedFlight, float radius, IPoolReciver<ArrowProjectile> arrowPool)
    {
        _speedFlight = speedFlight;
        _radius = radius;
        _poolReciver = arrowPool;
    }*/
}