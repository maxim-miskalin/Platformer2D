using UnityEngine;
using UnityEngine.Events;

public class Pocket : MonoBehaviour
{
    private float _coinsCounter = 0;

    public UnityEvent<float> ValueChanged;

    private void Start()
    {
        ValueChanged.Invoke(_coinsCounter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _coinsCounter += coin.ToDestroyed();
            ValueChanged.Invoke(_coinsCounter);
        }
    }
}