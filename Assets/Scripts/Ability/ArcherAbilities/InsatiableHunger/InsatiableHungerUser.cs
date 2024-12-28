using System.Collections;
using UnityEngine;

namespace Ability.ArcherAbilities.InsatiableHunger
{
    public class InsatiableHungerUser : MonoBehaviour
    {
        private InsatiableHunger _insatiableHunger;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;

        public float CooldownTime { get; private set; }

        public void Upgrade(InsatiableHunger insatiableHunger)
        {
            _insatiableHunger = insatiableHunger;
        }

        public IEnumerator UseAbility(IVampirismable vampirismable)
        {
            //Debug.Log(_insatiableHunger.CooldownTime + " Cooldown");
            float duration = 0;
            vampirismable.SetCoefficient(_insatiableHunger.Vampirism);
            //Debug.Log(_insatiableHunger.Vampirism + " Vampirism");

            if (Time.time >= _lastUsedTimer + _insatiableHunger.CooldownTime || _canUseFirstTime)
            {
                while (duration < _insatiableHunger.Duration)//��������
                {
                    vampirismable.SetTrueVampirismState();
                    duration += Time.deltaTime;
                    _lastUsedTimer = Time.time;
                    _canUseFirstTime = false;

                    yield return null;
                }

                vampirismable.SetFalseVampirismState();

                CooldownTime = _lastUsedTimer + _insatiableHunger.CooldownTime - Time.time;//����� ������� ������������ ��������
            }
            else
            {
                //Debug.Log("�������� " + (_lastUsedTimer + _insatiableHunger.CooldownTime - Time.time));
            }
        }
    }
}
