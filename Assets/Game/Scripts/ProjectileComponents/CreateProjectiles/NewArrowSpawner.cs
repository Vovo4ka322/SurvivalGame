using UnityEngine;
using Game.Scripts.Interfaces;
using Game.Scripts.PoolComponents;
using Game.Scripts.Weapons.RangedWeapon;

namespace Game.Scripts.ProjectileComponents.CreateProjectiles
{
    public class NewArrowSpawner : MonoBehaviour, IArrowPoolReturner
    {
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private PoolSettings _poolSettings;

        private BasePool<Arrow> _arrowPool;

        private void Awake()
        {
            _arrowPool = new BasePool<Arrow>(_arrowPrefab, _poolSettings, null);
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