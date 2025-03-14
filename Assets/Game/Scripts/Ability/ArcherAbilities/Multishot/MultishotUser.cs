using System;
using System.Collections;
using UnityEngine;
using Game.Scripts.Interfaces;
using Weapons.RangedWeapon;
using System.Collections.Generic;

namespace Ability.ArcherAbilities.Multishot
{
    public class MultishotUser : MonoBehaviour, ICooldownable
    {
        [SerializeField] private NewArrowSpawner _arrowSpawner;
        [SerializeField] private Bow _bow;
        [SerializeField] private Cooldown _cooldown;

        private Multishot _multishotScriptableObject;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;
        private IEnemyHitHandler _enemyHitHandler;

        public event Action<float> Used;

        public Multishot Multishot => _multishotScriptableObject;

        public float CooldownTime { get; private set; }

        public void Upgrade(Multishot multishot)
        {
            _multishotScriptableObject = multishot;
        }

        public void SetHandler(IEnemyHitHandler enemyHitHandler)
        {
            _enemyHitHandler = enemyHitHandler;
        }

        public IEnumerator UseAbility(float value)
        {
            float duration = 0;

            if (Time.time >= _lastUsedTimer + _multishotScriptableObject.CooldownTime || _canUseFirstTime)
            {
                while (duration < _multishotScriptableObject.Duration)
                {
                    _bow.SetTrueActiveState();

                    while (_cooldown.CanUse)
                    {
                        CalculateArrowFlight(value);

                        _cooldown.LaunchTimer(_multishotScriptableObject.Delay);
                    }

                    duration += Time.deltaTime;
                    _lastUsedTimer = Time.time;
                    _canUseFirstTime = false;

                    yield return null;
                }

                StartCoroutine(StartCooldown());
                _bow.SetFalseActiveState();
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

        private void CalculateArrowFlight(float value)
        {
            int coefficient = 2;
            int oneArrow = 1;

            float facingRotation = Mathf.Atan2(_bow.transform.position.y, _bow.transform.position.x) * Mathf.Rad2Deg;
            float startRotation = facingRotation + _multishotScriptableObject.SpreadAngle / coefficient;
            float angleIncrease = _multishotScriptableObject.SpreadAngle / (_multishotScriptableObject.ArrowCount - oneArrow);

            for (int i = 0; i < _multishotScriptableObject.ArrowCount; i++)
            {
                float tempRotation = startRotation - angleIncrease * i;
                Arrow arrow = _arrowSpawner.Spawn();
                arrow.StartFly(Quaternion.Euler(0, tempRotation, 0) * -_bow.StartPointToFly.forward, _bow.StartPointToFly.position);
                arrow.Weapon.SetTotalDamage(value);
                arrow.SetHandler(_enemyHitHandler);
            }
        }
    }
}
