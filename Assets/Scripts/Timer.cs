using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _waveTime;//длительность волны + время старта волны
    private float _startTime;

    public event Action WaveChanged;

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        if(_waveTime < Time.time)
            WaveChanged?.Invoke();     
        
        Debug.Log(Time.time - _startTime);
    }

    public void SetWaveTime(float waveTime)
    {
        _waveTime = waveTime + Time.time;
    }
}
