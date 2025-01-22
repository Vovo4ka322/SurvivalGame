using Pools;
using UnityEngine;

namespace Weapons.RangedWeapon
{
    public class ArrowSpawner : MonoBehaviour
    {
        [SerializeField] private Pool<Arrow> _pool;

        public Arrow Spawn(Transform transform, Quaternion quaternion, float arrowFlightSpeed, float flightRadius)
        {
            Arrow arrow = _pool.Get(transform, quaternion);
            arrow.gameObject.SetActive(true);
            arrow.Init(arrowFlightSpeed, flightRadius, _pool);

            return arrow;
        }
    }
}