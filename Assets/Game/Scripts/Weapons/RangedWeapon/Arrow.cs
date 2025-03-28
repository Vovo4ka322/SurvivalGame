using Game.Scripts.EnemyComponents;
using Game.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Weapons.RangedWeapon
{
    public class Arrow : MonoBehaviour
    {
        private Vector3 _direction;
        private Coroutine _coroutine;
        private IArrowPoolReturner _arrowPool;
        private IEnemyHitHandler _enemyHitHandler;

        [field: SerializeField] public ArrowData ArrowData { get; private set; }
        [field: SerializeField] public Weapon Weapon { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.ChangeHealth(Weapon.TotalDamage);
                _enemyHitHandler.OnHealthRestored();
            }

            _arrowPool.OnPoolReturned(this);
        }

        public void SetReturner(IArrowPoolReturner arrowPoolReturner)
        {
            _arrowPool = arrowPoolReturner;
        }

        public void SetHandler(IEnemyHitHandler enemyHitHandler)
        {
            _enemyHitHandler = enemyHitHandler;
        }

        public void StartFly(Vector3 direction, Vector3 position)
        {
            transform.position = position;
            _direction = direction.normalized;
            transform.forward = _direction;

            _coroutine = StartCoroutine(Fly());
        }

        private IEnumerator Fly()
        {
            float distanceTravelled = 0;

            while (distanceTravelled < ArrowData.AttackRadius)
            {
                float step = ArrowData.ArrowFlightSpeed * Time.deltaTime;
                transform.position += _direction * step;
                distanceTravelled += step;

                yield return null;
            }

            _arrowPool.OnPoolReturned(this);
        }
    }
}