using System;
using System.Collections;
using UnityEngine;
using Game.Scripts.Interfaces;

namespace Game.Scripts.AbilityComponents.MeleeAbilities.BorrowedTimeAbility
{
    public class BorrowedTimeUser : MonoBehaviour, ICooldownable
    {
        private BorrowedTime _borrowedTimeScriptableObject;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;

        public event Action<float> Used;

        public float CooldownTime { get; private set; }

        public BorrowedTime BorrowedTime => _borrowedTimeScriptableObject;

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

                StartCoroutine(StartCooldown());
                healable.SetFalseActiveState();
            }
            else
            {
                Debug.Log("Осталось " + (_lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime - Time.time));
            }
        }

        private IEnumerator StartCooldown()
        {
            CooldownTime = _lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime - Time.time;

            while (CooldownTime > 0)
            {
                CooldownTime = _lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime - Time.time;
                Used?.Invoke(CooldownTime);

                yield return null;
            }
        }
    }
}