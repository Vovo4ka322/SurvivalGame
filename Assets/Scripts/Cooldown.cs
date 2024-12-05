using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public bool CanUse { get; private set; }

    private void Awake() => CanUse = true;

    public void LaunchTimer(float time) => StartCoroutine(StartTimer(time));

    private IEnumerator StartTimer(float time)
    {
        WaitForSeconds duration = new(time);

        CanUse = false;

        yield return duration;

        CanUse = true;
    }
}
