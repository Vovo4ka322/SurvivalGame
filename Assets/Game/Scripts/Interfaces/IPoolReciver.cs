using UnityEngine;

namespace Game.Scripts.Interfaces
{
    public interface IPoolReciver<T> where T: MonoBehaviour
    {
        public void Release(T obj);
    }
}