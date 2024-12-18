using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class BladeFuryUser : MonoBehaviour, ICooldownable
    {
        private BladeFury _bladeFuryScriptableObject;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;

        public float CooldownTime { get; private set; }

        public void Upgrade(BladeFury bladeFury)
        {
            _bladeFuryScriptableObject = bladeFury;
        }

        public IEnumerator UseAbility(Transform player)
        {
            Debug.Log(_bladeFuryScriptableObject.CooldownTime + " Cooldown");
            float duration = 0;

            if (Time.time >= _lastUsedTimer + _bladeFuryScriptableObject.CooldownTime || _canUseFirstTime)
            {
                while (duration < _bladeFuryScriptableObject.Duration)
                {
                    player.Rotate(Vector3.up, _bladeFuryScriptableObject.TurnSpeed * Time.deltaTime);
                    duration += Time.deltaTime;
                    _lastUsedTimer = Time.time;
                    _canUseFirstTime = false;

                    yield return null;
                }

                CooldownTime = _lastUsedTimer + _bladeFuryScriptableObject.CooldownTime - Time.time;//����� ������� ������������ ��������
            }
            else
            {
                Debug.Log("�������� " + (_lastUsedTimer + _bladeFuryScriptableObject.CooldownTime - Time.time));
            }
        }
    }
}