using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Game.Scripts.SettingsComponents
{
    public class GarbageGenerator : MonoBehaviour
    {
        private void Update()
        {
            Profiler.BeginSample("Trasher");
            
            for (int i = 0; i < 500; i++)
            {
                List<int> garbages = new List<int>(200);
            }
            
            Profiler.EndSample();
        }
    }
}