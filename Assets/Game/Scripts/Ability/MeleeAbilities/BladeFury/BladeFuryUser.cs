using System;
using System.Collections;
using UnityEngine;

namespace Ability.MeleeAbilities.BladeFury
{
    public class BladeFuryUser : MonoBehaviour, ICooldownable
    {
        [SerializeField] private AnimatorState _animator;

        private BladeFury _bladeFuryScriptableObject;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;

        public event Action<float> Used;

        public float CooldownTime { get; private set; }

        public BladeFury BladeFury => _bladeFuryScriptableObject;

        public void Upgrade(BladeFury bladeFury)
        {
            _bladeFuryScriptableObject = bladeFury;
        }

        public IEnumerator UseAbility(Transform player)
        {
            float duration = 0;

            if (Time.time >= _lastUsedTimer + _bladeFuryScriptableObject.CooldownTime || _canUseFirstTime)
            {
                while (duration < _bladeFuryScriptableObject.Duration)
                {
                    _animator.SetTrueBoolState(_animator.CanUseSkill1Hash);
                    _animator.SetFalseBoolState(_animator.IsAttack);

                    player.Rotate(Vector3.up, _bladeFuryScriptableObject.TurnSpeed * Time.deltaTime);
                    duration += Time.deltaTime;
                    _lastUsedTimer = Time.time;
                    _canUseFirstTime = false;

                    yield return null;
                }

                _animator.SetFalseBoolState(_animator.CanUseSkill1Hash);
                _animator.SetTrueBoolState(_animator.IsAttack);

                StartCoroutine(StartCooldown());
            }
        }

        private IEnumerator StartCooldown()
        {
            CooldownTime = _lastUsedTimer + _bladeFuryScriptableObject.CooldownTime - Time.time;

            while (CooldownTime > 0)
            {
                CooldownTime = _lastUsedTimer + _bladeFuryScriptableObject.CooldownTime - Time.time;
                Used?.Invoke(CooldownTime);

                yield return null;
            }
        }
    }
}