using System;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    private float _coinsCounter = 0;

    public event Action<float> ValueChanged;

    private void Start()
    {
        ValueChanged?.Invoke(_coinsCounter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _coinsCounter += coin.Destroy();
            ValueChanged.Invoke(_coinsCounter);
        }
    }
}