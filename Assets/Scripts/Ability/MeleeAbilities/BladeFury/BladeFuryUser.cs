using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeFuryUser : MonoBehaviour
{
    private BladeFury _bladeFuryScr;

    public void Upgrade(BladeFury bladeFury)
    {
        _bladeFuryScr = bladeFury;
    }

    public IEnumerator UseAbility()
    {
        float duration = 0;

        while (duration < _bladeFuryScr.Duration)
        {
            transform.Rotate(Vector3.up, _bladeFuryScr.TurnSpeed * Time.deltaTime);
            duration += Time.deltaTime;

            yield return null;
        }
    }
}
