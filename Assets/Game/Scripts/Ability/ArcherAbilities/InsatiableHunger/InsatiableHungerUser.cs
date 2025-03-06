using System;
using System.Collections;
using UnityEngine;
using Game.Scripts.Interfaces;

namespace Ability.ArcherAbilities.InsatiableHunger
{
    public class InsatiableHungerUser : MonoBehaviour
    {
        private InsatiableHunger _insatiableHunger;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;

        public event Action<float> Used;

        public float CooldownTime { get; private set; }

        public InsatiableHunger InsatiableHunger => _insatiableHunger;

        public void Upgrade(InsatiableHunger insatiableHunger)
        {
            _insatiableHunger = insatiableHunger;
        }

        public IEnumerator UseAbility(IVampirismable vampirismable)
        {
            float duration = 0;
            vampirismable.SetCoefficient(_insatiableHunger.Vampirism);
            Debug.Log(_insatiableHunger.Vampirism + " _insatiableHunger.Vampirism");
            if (Time.time >= _lastUsedTimer + _insatiableHunger.CooldownTime || _canUseFirstTime)
            {
                while (duration < _insatiableHunger.Duration)
                {
                    vampirismable.SetTrueVampirismState();
                    duration += Time.deltaTime;
                    _lastUsedTimer = Time.time;
                    _canUseFirstTime = false;

                    yield return null;
                }

                StartCoroutine(StartCooldown());
                vampirismable.SetFalseVampirismState();
            }
        }

        private IEnumerator StartCooldown()
        {
            CooldownTime = _lastUsedTimer + _insatiableHunger.CooldownTime - Time.time;

            while (CooldownTime > 0)
            {
                CooldownTime = _lastUsedTimer + _insatiableHunger.CooldownTime - Time.time;
                Used?.Invoke(CooldownTime);

                yield return null;
            }
        }
    }
}
