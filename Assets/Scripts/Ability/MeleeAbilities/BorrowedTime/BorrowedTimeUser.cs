using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorrowedTimeUser : MonoBehaviour
{
    [SerializeField] private BorrowedTime _borrowedTime;

    public float Duration { get; private set; }

    public bool IsWorking { get; private set; } = false;

    public IEnumerator UseAbility(float enemyDamage, Player player)
    {
        Duration = 0;

        while (Duration < _borrowedTime.Duration)
        {
            player.Health.Add(enemyDamage);
            Duration += Time.deltaTime;

            yield return null;
        }

        IsWorking = false;
    }

    public bool True() => IsWorking = true;
}
