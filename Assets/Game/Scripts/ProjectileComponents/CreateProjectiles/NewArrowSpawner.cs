using UnityEngine;
using Game.Scripts.PoolComponents;
using Game.Scripts.Weapons.RangedWeapon;

namespace Weapons.RangedWeapon
{
    public class NewArrowSpawner : MonoBehaviour, IArrowPoolReturner
    {
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private PoolSettings _poolSettings;

        private ArrowPool _arrowPool;

        private void Awake()
        {
            _arrowPool = new(_arrowPrefab, _poolSettings, null);
        }

        public Arrow Spawn()
        {
            Arrow arrow = _arrowPool.Get();
            arrow.transform.SetPositionAndRotation(transform.position, transform.rotation);
            arrow.SetReturner(this);

            return arrow;
        }

        public void OnPoolReturned(Arrow arrow)
        {
            _arrowPool.Release(arrow);
        }
    }
}

public interface IArrowPoolReturner
{
    public void OnPoolReturned(Arrow arrow);
}