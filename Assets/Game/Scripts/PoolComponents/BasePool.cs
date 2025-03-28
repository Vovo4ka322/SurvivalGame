using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PoolComponents
{
    public class BasePool<T>
        where T : Component
    {
        private readonly Stack<T> _pool = new Stack<T>();
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly int _maxPoolSize;
        private readonly bool _collectionCheck;

        private int _countAll;

        public BasePool(T prefab, PoolSettings settings, Transform container = null)
        {
            _prefab = prefab;
            _container = container;
            _maxPoolSize = settings.MaxPoolSize;
            _collectionCheck = settings.CollectionCheck;

            Preload(settings.InitialCapacity);
        }

        public T Get()
        {
            T instance;

            if (_pool.Count > 0)
            {
                instance = _pool.Pop();
            }
            else
            {
                if (_maxPoolSize > 0 && _countAll >= _maxPoolSize)
                {
                    return null;
                }

                instance = CreateNewInstance();
            }

            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Release(T instance)
        {
            if (instance == null)
            {
                return;
            }

            instance.transform.SetParent(_container, false);
            instance.gameObject.SetActive(false);

            if (_collectionCheck && _pool.Contains(instance))
            {
                return;
            }

            _pool.Push(instance);
        }

        private T CreateNewInstance()
        {
            T instance = Object.Instantiate(_prefab, _container);

            instance.gameObject.SetActive(false);
            _countAll++;

            return instance;
        }

        private void Preload(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T instance = CreateNewInstance();
                Release(instance);
            }
        }
    }
}