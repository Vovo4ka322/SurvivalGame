using System;
using UnityEngine;
using Game.Scripts.Interfaces;
using static UnityEngine.Rendering.DebugUI;

namespace Weapons.RangedWeapon
{
    public class Bow : Weapon, IActivable
    {
        [SerializeField] private NewArrowSpawner _arrowSpawner;
        [SerializeField] private ArrowData _bowData;

        private Arrow _arrow;

        public event Action ArrowTouched;

        [field: SerializeField] public Transform StartPointToFly { get; private set; }

        public ArrowData BowData => _bowData;

        public bool IsActiveState { get; private set; }

        public void ArrowSetTotalDamage(float value)
        {
            _arrow.Weapon.SetTotalDamage(value);
        }

        public void StartShoot(float value)
        {
            Shoot(value);
        }

        public void OnTouched()
        {
            ArrowTouched?.Invoke();
        }

        public bool SetTrueActiveState() => IsActiveState = true;

        public bool SetFalseActiveState() => IsActiveState = false;

        private void Shoot(float value)
        {
            if (IsActiveState == false)
            {
                if (_arrow != null)
                    _arrow.Touched -= OnTouched;

                Arrow arrow = _arrowSpawner.Spawn(_bowData.ArrowFlightSpeed, _bowData.AttackRadius);
                arrow.StartFly(StartPointToFly.forward, StartPointToFly.position);
                _arrow = arrow;
                _arrow.Weapon.SetTotalDamage(value);
                _arrow.Touched += OnTouched;
            }
        }
    }
}