using System.Collections;
using UnityEngine;
using EnemyComponents.Interfaces;

namespace EnemyComponents.EnemySettings
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        Coroutine ICoroutineRunner.StartCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        void ICoroutineRunner.StopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        void ICoroutineRunner.StopAllCoroutines()
        {
            StopAllCoroutines();
        }
    }
}