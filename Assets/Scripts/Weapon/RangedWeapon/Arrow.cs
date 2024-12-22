using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 _direction;
    private IPoolReciver<Arrow> _poolReciver;
    private Coroutine _coroutine;
    private float _speedFlight;
    private float _radius;

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