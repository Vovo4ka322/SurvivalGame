using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private ArrowSpawner _arrowSpawner;
    [SerializeField] private BowData _bowData;

    private Coroutine _arrowCreatorCoroutine;

    [field: SerializeField] public bool IsActive { get; private set; }

    private void Start()
    {
        _arrowCreatorCoroutine = StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds timeToSpawn = new(_bowData.AttackSpeed);

        while (IsActive == false)
        { 
            Arrow arrow = _arrowSpawner.Spawn(transform, Quaternion.identity);
            arrow.StartFly(transform.forward, transform.position);

            yield return timeToSpawn;
        }
    }
}
