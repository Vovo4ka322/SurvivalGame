using System.Collections;
using UnityEngine;

namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        
        public void StopCoroutine(Coroutine coroutine);
        
        public void StopAllCoroutines();
    }
}