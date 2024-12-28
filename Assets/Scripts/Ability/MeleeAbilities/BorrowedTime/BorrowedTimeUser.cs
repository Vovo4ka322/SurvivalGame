using System.Collections;
using UnityEngine;
namespace Ability.MeleeAbilities.BorrowedTime
{
    public class BorrowedTimeUser : MonoBehaviour, ICooldownable
    {
        private BorrowedTime _borrowedTimeScriptableObject;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;

        public float CooldownTime { get; private set; }

        public void Upgrade(BorrowedTime borrowedTime)
        {
            _borrowedTimeScriptableObject = borrowedTime;
        }

        public IEnumerator UseAbility(IActivable healable)
        {
            Debug.Log(_borrowedTimeScriptableObject.CooldownTime + " Cooldown");
            float duration = 0;

            if (Time.time >= _lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime || _canUseFirstTime)
            {
                while (duration < _borrowedTimeScriptableObject.Duration)//где-то тут на время действия способности добавить партикл
                {
                    healable.SetTrueActiveState();
                    duration += Time.deltaTime;
                    _lastUsedTimer = Time.time;
                    _canUseFirstTime = false;

                    yield return null;
                }

                healable.SetFalseActiveState();

                CooldownTime = _lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime - Time.time;
            }
            else
            {
                Debug.Log("Осталось " + (_lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime - Time.time));
            }
        }
    }
}