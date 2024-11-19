using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour
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
    }

    public T Get()
    {
        if(_objectStorage.Count != 0)
        {
            T firstElement = _objectStorage[0];
            _objectStorage.Remove(firstElement);

            return firstElement;
        }

        T objectForReturn = CreateObject(_object);
        objectForReturn.gameObject.SetActive(false);

        return objectForReturn;
    }

    private T CreateObject(T objectCreator) => Instantiate(objectCreator);
}