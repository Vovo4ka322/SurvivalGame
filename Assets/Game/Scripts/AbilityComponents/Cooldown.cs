using System.Collections;
using UnityEngine;

namespace Game.Scripts.AbilityComponents
{
    public class Cooldown
    {
        public bool CanUse { get; private set; } = true;

        public IEnumerator StartTimer(float time)
        {
            WaitForSeconds duration = new (time);

            CanUse = false;

            yield return duration;

            CanUse = true;
        }
    }
}