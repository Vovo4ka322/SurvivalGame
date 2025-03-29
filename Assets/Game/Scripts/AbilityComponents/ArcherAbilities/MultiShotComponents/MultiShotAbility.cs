using System;
using System.Collections;
using UnityEngine;
using Game.Scripts.Interfaces;
using Game.Scripts.ProjectileComponents.CreateProjectiles;
using Game.Scripts.Weapons.RangedWeapon;

namespace Game.Scripts.AbilityComponents.ArcherAbilities.MultiShotComponents
{
    public class MultiShotAbility : MonoBehaviour, ICooldownable
    {
        [SerializeField] private NewArrowSpawner _arrowSpawner;
        [SerializeField] private Bow _bow;

        private Cooldown _cooldown;
        private MultiShot _multiShotScriptableObject;
        private float _lastUsedTimer = 0;
        private bool _canUseFirstTime = true;
        private IEnemyHitHandler _enemyHitHandler;

        public event Action<float> Used;

        public MultiShot MultiShot => _multiShotScriptableObject;
        public float CooldownTime { get; private set; }

        private void Awake()
        {
            _cooldown = new Cooldown();
        }

        public void ReplaceValue(MultiShot multiShot)
        {
            _multiShotScriptableObject = multiShot;
        }

        public void SetHandler(IEnemyHitHandler enemyHitHandler)
        {
            _enemyHitHandler = enemyHitHandler;
        }

        public IEnumerator UseAbility(float value)
        {
            float duration = 0;

            if (Time.time >= _lastUsedTimer + _multiShotScriptableObject.CooldownTime || _canUseFirstTime)
            {
                while (duration < _multiShotScriptableObject.Duration)
                {
                    _bow.SetTrueActiveState();

                    while (_cooldown.CanUse)
                    {
                        CalculateArrowFlight(value);

                        _cooldown.StartTimer(_multiShotScriptableObject.Delay);
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
            CooldownTime = _lastUsedTimer + _multiShotScriptableObject.CooldownTime - Time.time;

            while (CooldownTime > 0)
            {
                CooldownTime = _lastUsedTimer + _multiShotScriptableObject.CooldownTime - Time.time;
                Used?.Invoke(CooldownTime);

                yield return null;
            }
        }

        private void CalculateArrowFlight(float value)
        {
            int coefficient = 2;
            int oneArrow = 1;

            float facingRotation = Mathf.Atan2(_bow.transform.position.y, _bow.transform.position.x) * Mathf.Rad2Deg;
            float startRotation = facingRotation + _multiShotScriptableObject.SpreadAngle / coefficient;
            float angleIncrease = _multiShotScriptableObject.SpreadAngle / (_multiShotScriptableObject.ArrowCount - oneArrow);

            for (int i = 0; i < _multiShotScriptableObject.ArrowCount; i++)
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
