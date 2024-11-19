using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field:SerializeField] public float Value {  get; private set; }

    public float MaxValue {  get; private set; }

    public event Action<float> Changed;

    public void Lose(float damage)
    {
        Value = Mathf.Clamp(Value - damage, 0, MaxValue);
        Changed?.Invoke(Value);
    }

    public void Add(float value)
    {
        Value = Mathf.Clamp(Value + value, 0, MaxValue);
        Changed?.Invoke(Value);
    }

    public void InitValue(float maxValue)
    {
        MaxValue = maxValue;
        Value = MaxValue;
    }
}
