using System;
using System.Collections;
using UnityEngine;

namespace Weapons.RangedWeapon
{
    public class Bow : Weapon, IActivable
    {
        [SerializeField] private ArrowSpawner _arrowSpawner;
        [SerializeField] private BowData _bowData;

        private Arrow _arrow;
        private Coroutine _arrowCreatorCoroutine;

        public event Action ArrowTouched;

        public BowData BowData => _bowData;

        public bool IsActiveState { get; private set; }

        private void Start()
        {
            StartShoot();
        }

        public void StartShoot()
        {
            _arrowCreatorCoroutine = StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {
            WaitForSeconds timeToSpawn = new(_bowData.AttackSpeed);

            while (IsActiveState == false)
            {
                yield return timeToSpawn;

                if (_arrow != null)
                    _arrow.Touched -= OnTouched;

                Arrow arrow = _arrowSpawner.Spawn(transform, Quaternion.identity, _bowData.ArrowFlightSpeed, _bowData.AttackRadius);
                arrow.StartFly(transform.forward, transform.position);
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