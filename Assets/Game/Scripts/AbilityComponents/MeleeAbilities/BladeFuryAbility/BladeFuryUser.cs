using System;
using System.Collections;
using UnityEngine;
using Game.Scripts.Interfaces;
using Game.Scripts.PlayerComponents.Animations;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.AbilityComponents.MeleeAbilities.BladeFuryAbility
{
    public class BladeFuryUser : MonoBehaviour, ICooldownable
    {
        [SerializeField] private AnimatorStatePlayer _animator;

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

        public IEnumerator UseAbility(MeleePlayer meleePlayer)
        {
            if (_bladeFuryScriptableObject != null)
            {
                float duration = 0;

                if (Time.time >= _lastUsedTimer + _bladeFuryScriptableObject.CooldownTime || _canUseFirstTime)
                {
                    while (duration < _bladeFuryScriptableObject.Duration)
                    {
                        _animator.SetTrueBoolState(_animator.CanUseSkill1Hash);
                        _animator.SetFalseBoolState(_animator.IsAttack);

                        meleePlayer.PlayerMovement.SetStateSkillWorkingTrue();
                        meleePlayer.SetSwordColliderTrue();
                        meleePlayer.transform.Rotate(Vector3.up, _bladeFuryScriptableObject.TurnSpeed * Time.deltaTime);
                        duration += Time.deltaTime;
                        _lastUsedTimer = Time.time;
                        _canUseFirstTime = false;

                        yield return null;
                    }

                    _animator.SetFalseBoolState(_animator.CanUseSkill1Hash);
                    _animator.SetTrueBoolState(_animator.IsAttack);
                    meleePlayer.PlayerMovement.SetStateSkillWorkingFalse();

                    StartCoroutine(StartCooldown());
                }
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