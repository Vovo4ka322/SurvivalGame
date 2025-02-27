using System;
using System.Collections;
using UnityEngine;
using Game.Scripts.Interfaces;
using Weapons.RangedWeapon;

namespace Ability.ArcherAbilities.Multishot
{
    public class MultishotUser : MonoBehaviour, ICooldownable
    {
        [SerializeField] private ArrowSpawner _arrowSpawner;
        [SerializeField] private Bow _bow;
        [SerializeField] private Cooldown _cooldown;

        private Multishot _multishotScriptableObject;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;
        private Arrow _arrow;

        public event Action<float> Used;

        public Multishot Multishot => _multishotScriptableObject;

        public float CooldownTime { get; private set; }

        public void Upgrade(Multishot multishot)
        {
            _multishotScriptableObject = multishot;
        }

        public IEnumerator UseAbility()
        {
            float duration = 0;

            if (Time.time >= _lastUsedTimer + _multishotScriptableObject.CooldownTime || _canUseFirstTime)
            {
                while (duration < _multishotScriptableObject.Duration)
                {
                    _bow.SetTrueActiveState();

                    while (_cooldown.CanUse)
                    {
                        CalculateArrowFlight();

                        _cooldown.LaunchTimer(_multishotScriptableObject.Delay);
                    }

                    duration += Time.deltaTime;
                    _lastUsedTimer = Time.time;
                    _canUseFirstTime = false;

                    yield return null;
                }

                StartCoroutine(StartCooldown());
                _bow.SetFalseActiveState();
                _bow.StartShoot();
            }
        }

        private IEnumerator StartCooldown()
        {
            CooldownTime = _lastUsedTimer + _multishotScriptableObject.CooldownTime - Time.time;

            while (CooldownTime > 0)
            {
                CooldownTime = _lastUsedTimer + _multishotScriptableObject.CooldownTime - Time.time;
                Used?.Invoke(CooldownTime);

                yield return null;
            }
        }

        private void CalculateArrowFlight()
        {
            if (_arrow != null)
                _arrow.Touched -= _bow.OnTouched;

            int coefficient = 2;
            int oneArrow = 1;

            float facingRotation = Mathf.Atan2(_bow.transform.position.y, _bow.transform.position.x) * Mathf.Rad2Deg;
            float startRotation = facingRotation + _multishotScriptableObject.SpreadAngle / coefficient;
            float angleIncrease = _multishotScriptableObject.SpreadAngle / (_multishotScriptableObject.ArrowCount - oneArrow);

            for (int i = 0; i < _multishotScriptableObject.ArrowCount; i++)
            {
                float tempRotation = startRotation - angleIncrease * i;
                Arrow arrow = _arrowSpawner.Spawn(_bow.BowData.ArrowFlightSpeed, _bow.BowData.AttackRadius);
                arrow.StartFly(Quaternion.Euler(0, tempRotation, 0) * -_bow.StartPointToFly.forward, _bow.StartPointToFly.position);
                _arrow = arrow;
                _arrow.Touched += _bow.OnTouched;
            }
        }
    }
}
