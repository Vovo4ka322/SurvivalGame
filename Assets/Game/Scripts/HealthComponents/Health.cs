using System;
using UnityEngine;

namespace Game.Scripts.HealthComponents
{
    public class Health : MonoBehaviour
    {
        public event Action<float> Changed;
        public event Action Death;

        public bool IsDead => Value <= 0;
        public float Value { get; private set; }
        public float MaxValue { get; private set; }

        public void Lose(float damage)
        {
            if (IsDead)
            {
                return;
            }

            Value = Mathf.Clamp(Value - damage, 0, MaxValue);

            Changed?.Invoke(Value);

            if (IsDead)
            {
                Death?.Invoke();
            }
        }

        public void Add(float value)
        {
            if (IsDead)
            {
                return;
            }

            Value = Mathf.Clamp(Value + value, 0, MaxValue);

            Changed?.Invoke(Value);
        }

        public void InitMaxValue(float maxValue)
        {
            MaxValue = maxValue;
            Value = MaxValue;

            Changed?.Invoke(Value);
        }
    }
}