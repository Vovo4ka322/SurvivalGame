using UnityEngine;
using Game.Scripts.PoolComponents;

namespace Weapons.RangedWeapon
{
    public class ArrowSpawner : MonoBehaviour
    {
        [SerializeField] private Pool<Arrow> _pool;

        public Arrow Spawn(float arrowFlightSpeed, float flightRadius)
        {
            Arrow arrow = _pool.Get(transform, transform.rotation);
            arrow.gameObject.SetActive(true);
            arrow.Init(arrowFlightSpeed, flightRadius, _pool);

            return arrow;
        }
    }
}