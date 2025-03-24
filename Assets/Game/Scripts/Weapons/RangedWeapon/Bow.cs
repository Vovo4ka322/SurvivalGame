using UnityEngine;
using Game.Scripts.Interfaces;
using Game.Scripts.ProjectileComponents.CreateProjectiles;

namespace Game.Scripts.Weapons.RangedWeapon
{
    public class Bow : Weapon, IActivable
    {
        [SerializeField] private NewArrowSpawner _arrowSpawner;
        [SerializeField] private ArrowData _bowData;

        private IEnemyHitHandler _enemyHitHandler;

        [field: SerializeField] public Transform StartPointToFly { get; private set; }

        public ArrowData BowData => _bowData;

        public bool IsActiveState { get; private set; }

        public void StartShoot(float value)
        {
            Shoot(value);
        }

        public void SetHandler(IEnemyHitHandler enemyHitHandler)
        {
            _enemyHitHandler = enemyHitHandler;
        }

        public bool SetTrueActiveState() => IsActiveState = true;

        public bool SetFalseActiveState() => IsActiveState = false;

        private void Shoot(float value)
        {
            if (IsActiveState == false)
            {
                Arrow arrow = _arrowSpawner.Spawn();
                arrow.StartFly(StartPointToFly.forward, StartPointToFly.position);
                arrow.SetHandler(_enemyHitHandler);
                arrow.Weapon.SetTotalDamage(value);
            }
        }
    }
}