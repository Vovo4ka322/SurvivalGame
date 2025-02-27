using System;
using UnityEngine;
using Game.Scripts.Interfaces;

namespace Weapons.RangedWeapon
{
    public class Bow : Weapon, IActivable
    {
        [SerializeField] private ArrowSpawner _arrowSpawner;
        [SerializeField] private BowData _bowData;

        private Arrow _arrow;
        private Coroutine _arrowCreatorCoroutine;

        public event Action ArrowTouched;

        [field: SerializeField] public Transform StartPointToFly {  get; private set; }

        public BowData BowData => _bowData;

        public bool IsActiveState { get; private set; }

        private void Start()
        {
            StartShoot();
        }

        public void StartShoot()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (IsActiveState == false)
            {
                if (_arrow != null)
                    _arrow.Touched -= OnTouched;

                Arrow arrow = _arrowSpawner.Spawn(_bowData.ArrowFlightSpeed, _bowData.AttackRadius);
                arrow.StartFly(StartPointToFly.forward, StartPointToFly.position);
                _arrow = arrow;
                _arrow.Touched += OnTouched;
            }
        }

        public void OnTouched()
        {
            ArrowTouched?.Invoke();
        }

        public bool SetTrueActiveState() => IsActiveState = true;

        public bool SetFalseActiveState() => IsActiveState = false;
    }
}