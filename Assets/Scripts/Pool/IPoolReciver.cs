using UnityEngine;

public interface IPoolReciver<T> where T: MonoBehaviour
{
    public void Release(T @object);
}