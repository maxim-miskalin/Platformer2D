using System;
using UnityEngine;
using UnityEngine.Events;

public class TargetDetection : MonoBehaviour
{
    public UnityEvent<Collider2D> Locate;

    [SerializeField] private float _detectionRadius = 6f;
    [SerializeField] private LayerMask _playerLayer;

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, _detectionRadius, _playerLayer);

        if (target != null)
        {
            if (CheckForHealth(target))
            {
                Locate.Invoke(target);
            }
            else
            {
                Locate.Invoke(null);
            }
        }
        else
        {
            Locate.Invoke(null);
        }
    }

    private bool CheckForHealth(Collider2D target)
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