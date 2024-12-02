using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorrowedTimeUser : MonoBehaviour
{
    private BorrowedTime _borrowedTime;

    public float Duration { get; private set; }

    public bool IsWorking { get; private set; }

    public void Upgrade(BorrowedTime borrowedTime)
    {
        _borrowedTime = borrowedTime;
    }

    public IEnumerator UseAbility()
    {
        Duration = 0;

        while (Duration < _borrowedTime.Duration)
        {
            Duration += Time.deltaTime;

            yield return null;
        }

        IsWorking = false;
    }

    public bool IsWorkingTrue() => IsWorking = true;
}
