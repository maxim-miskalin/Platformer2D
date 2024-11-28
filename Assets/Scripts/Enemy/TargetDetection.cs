using System;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 6f;
    [SerializeField] private LayerMask _playerLayer;

    public event Action<Collider2D> Locate;

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, _detectionRadius, _playerLayer);

        if (target != null)
        {
            if (DiscoverHealth(target))
            {
                Locate?.Invoke(target);
            }
            else
            {
                Locate?.Invoke(null);
            }
        }
        else
        {
            Locate?.Invoke(null);
        }
    }

    private bool DiscoverHealth(Collider2D target)
    {
        if (target != null && target.TryGetComponent(out Health health))
        {
            return health.CurrentValue > 0;
        }
        else
        {
            return false;
        }
    }
}