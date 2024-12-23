using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IActivable
{
    [SerializeField] private ArrowSpawner _arrowSpawner;
    [SerializeField] private BowData _bowData;

    private Coroutine _arrowCreatorCoroutine;

    public bool IsActiveState {  get; private set; }

    private void Start()
    {
        StartShoot();
    }

    public void StartShoot()
    {
        _arrowCreatorCoroutine = StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds timeToSpawn = new(_bowData.AttackSpeed);

        while (IsActiveState == false)
        { 
            yield return timeToSpawn;

            Arrow arrow = _arrowSpawner.Spawn(transform, Quaternion.identity);
            arrow.StartFly(transform.forward, transform.position);
        }
    }

    public void SetState(bool state)
    {
        IsActiveState = state;
    }
}
