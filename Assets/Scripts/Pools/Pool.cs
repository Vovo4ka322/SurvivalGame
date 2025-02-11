using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public class Pool<T> : MonoBehaviour, IPoolReciver<T> where T : MonoBehaviour
    {
        [SerializeField] private T _object;

        private List<T> _objectStorage = new();
        
        public void Release(T @object)
        {
            if(_objectStorage.Contains(@object) == false)
            {
                _objectStorage.Add(@object);
                @object.gameObject.SetActive(false);
            }

            @object.transform.position = Vector3.zero;
            @object.transform.rotation = Quaternion.identity;
        }

        public T Get(Transform spawnTransform, Quaternion spawnRotation)
        {
            if(_objectStorage.Count != 0)
            {
                T firstElement = _objectStorage[0];
                _objectStorage.Remove(firstElement);

                firstElement.transform.position = spawnTransform.position;
                firstElement.transform.rotation = spawnRotation;

                return firstElement;
            }

            T objectForReturn = CreateObject(_object, spawnTransform, spawnRotation);
            objectForReturn.gameObject.SetActive(false);

            return objectForReturn;
        }

        private T CreateObject(T prefab, Transform spawnTransform, Quaternion spawnRotation)
        {
            return Instantiate(prefab, spawnTransform.position, spawnRotation);
        }
    }
}