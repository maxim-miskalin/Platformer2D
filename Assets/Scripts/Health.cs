using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxValue;

    public event Action<float, float> ValueChanged;
    public event Action<Health> Died;
    public float CurrentValue { get; private set; }

    private void Awake()
    {
        CurrentValue = _maxValue;
    }

    private void Start()
    {
        ValueChanged?.Invoke(CurrentValue, _maxValue);
    }

    public void TakeDamage(float damage)
    {
        if (CurrentValue > 0)
        {
            CurrentValue = Math.Clamp(CurrentValue - damage, 0, _maxValue);

            ValueChanged?.Invoke(CurrentValue, _maxValue);
        }

        if (CurrentValue == 0)
            Died?.Invoke(this);
    }

    public void RestoreValue(float health)
    {
        if (CurrentValue > 0)
        {
            CurrentValue = Math.Clamp(CurrentValue + health, 0, _maxValue);

            ValueChanged?.Invoke(CurrentValue, _maxValue);
        }
    }
}
