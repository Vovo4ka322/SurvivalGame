using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeFuryUser : MonoBehaviour
{
    [SerializeField] private BladeFury _bladeFury;

    public IEnumerator UseAbility()
    {
        if (Input.GetKeyDown(_bladeFury.KeyCode))
        {
            float duration = 0;

            while (duration < _bladeFury.Duration)
            {
                transform.Rotate(Vector3.up, _bladeFury.TurnSpeed * Time.deltaTime);
                duration += Time.deltaTime;

                yield return null;
            }
        }
    }
}
