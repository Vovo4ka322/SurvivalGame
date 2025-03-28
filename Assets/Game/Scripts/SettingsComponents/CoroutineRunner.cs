using Game.Scripts.EnemyComponents.Interfaces;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.SettingsComponents
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