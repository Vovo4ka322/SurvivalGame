using System.Collections;
using UnityEngine;

namespace Abilities
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

        public IEnumerator UseAbility(IHealable healable)
        {
            Debug.Log(_borrowedTimeScriptableObject.CooldownTime + " Cooldown");
            float duration = 0;

            if (Time.time >= _lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime || _canUseFirstTime)
            {
                while (duration < _borrowedTimeScriptableObject.Duration)//���-�� ��� �� ����� �������� ����������� �������� �������
                {
                    healable.SetState(true);
                    duration += Time.deltaTime;
                    _lastUsedTimer = Time.time;
                    _canUseFirstTime = false;

                    yield return null;
                }

                healable.SetState(false);

                CooldownTime = _lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime - Time.time;
            }
            else
            {
                Debug.Log("�������� " + (_lastUsedTimer + _borrowedTimeScriptableObject.CooldownTime - Time.time));
            }
        }
    }
}