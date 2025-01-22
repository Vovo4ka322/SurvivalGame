using System.Collections;
using UnityEngine;
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

        public float CooldownTime { get; private set; }

        public void Upgrade(Multishot multishot)
        {
            _multishotScriptableObject = multishot;
        }

        public IEnumerator UseAbility()
        {
            Debug.Log(_multishotScriptableObject.CooldownTime + " Cooldown");
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

                _bow.SetFalseActiveState();
                _bow.StartShoot();

                CooldownTime = _lastUsedTimer + _multishotScriptableObject.CooldownTime - Time.time;//����� ������� ������������ ��������
            }
            else
            {
                Debug.Log("�������� " + (_lastUsedTimer + _multishotScriptableObject.CooldownTime - Time.time));
            }
        }

        public void CalculateArrowFlight()
        {
            if(_arrow != null)
                _arrow.Touched -= _bow.OnTouched;

            int coefficient = 2;
            int oneArrow = 1;

            float facingRotation = Mathf.Atan2(_bow.transform.position.y, _bow.transform.position.x) * Mathf.Rad2Deg;
            float startRotation = facingRotation + _multishotScriptableObject.SpreadAngle / coefficient;
            float angleIncrease = _multishotScriptableObject.SpreadAngle / (_multishotScriptableObject.ArrowCount - oneArrow);

            for (int i = 0; i < _multishotScriptableObject.ArrowCount; i++)
            {
                float tempRotation = startRotation - angleIncrease * i;
                Arrow arrow = _arrowSpawner.Spawn(_bow.transform, Quaternion.Euler(0, 0, tempRotation), _bow.BowData.ArrowFlightSpeed, _bow.BowData.AttackRadius);
                arrow.StartFly(Quaternion.Euler(0, tempRotation, 0) * _bow.transform.forward, _bow.transform.position);
                _arrow = arrow;
                _arrow.Touched += _bow.OnTouched;
            }
        }
    }
}
