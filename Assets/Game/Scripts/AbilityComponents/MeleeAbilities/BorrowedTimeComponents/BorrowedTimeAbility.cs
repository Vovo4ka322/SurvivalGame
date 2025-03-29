using System;
using System.Collections;
using UnityEngine;
using Game.Scripts.Interfaces;

namespace Game.Scripts.AbilityComponents.MeleeAbilities.BorrowedTimeComponents
{
    public class BorrowedTimeAbility : MonoBehaviour, ICooldownable
    {
        private BorrowedTime _borrowedTimeScriptableObject;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;

        public event Action<float> Used;

        public float CooldownTime { get; private set; }
        public BorrowedTime BorrowedTime => _borrowedTimeScriptableObject;

        public void ReplaceValue(BorrowedTime borrowedTime)
        {
            _borrowedTimeScriptableObject = borrowedTime;
        }

        public IEnumerator UseAbility(IActivable healable)
        {
            if (_borrowedTimeScriptableObject != null)
            {
                float duration = 0;

                if (Time.time >= _lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime || _canUseFirstTime)
                {
                    while (duration < _borrowedTimeScriptableObject.Duration)
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