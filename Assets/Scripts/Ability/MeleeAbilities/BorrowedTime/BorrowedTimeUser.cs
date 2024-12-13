using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class BorrowedTimeUser : MonoBehaviour, ICooldownable
    {
        private BorrowedTime _borrowedTimeScriptableObject;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;

        public float Duration { get; private set; }

        public bool IsWorking { get; private set; }

        public float CooldownTime { get; private set; }

        public void Upgrade(BorrowedTime borrowedTime)
        {
            _borrowedTimeScriptableObject = borrowedTime;
        }

        public IEnumerator UseAbility(IHealable healable)
        {
            Duration = 0;

            if (Time.time >= _lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime || _canUseFirstTime)
            {
                healable.SetState(true);

                while (Duration < _borrowedTimeScriptableObject.Duration)//где-то тут на время действия способности добавить партикл
                {
                    IsWorking = true;
                    Duration += Time.deltaTime;

                    yield return null;
                }

                healable.SetState(false);

                CooldownTime = _lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime - Time.time;
            }

            IsWorking = false;
        }


    }
}