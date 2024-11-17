using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetDetection))]
public class MoverSlime : Mover
{
    [SerializeField] private float _minDistantionToPoint = 0.5f;

    private Transform _wayPoint;
    private TargetDetection _targetDetection;
    private List<Transform> _way = new();
    private Transform _target;
    private int _index = 0;

    private void Awake()
    {
        _targetDetection = GetComponent<TargetDetection>();
    }

    private void OnEnable()
    {
        _targetDetection.Locate += MovingTowardsGoal;
    }

    private void OnDisable()
    {
        _targetDetection.Locate -= MovingTowardsGoal;
    }

    private void Start()
    {
        for (int i = 0; i < _wayPoint.childCount; i++)
            _way.Add(_wayPoint.GetChild(i));

        MovingTowardsGoal(null);
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, _target.position) > _minDistantionToPoint)
        {
            if (_checkGround.IsGround)
            {
                Vector2 direction = (_target.position - transform.position).normalized;
                direction.Normalize();
                Move(direction);
            }

            if (_checkGround.IsGround)
                Jump();
        }
        else
        {
            _index = ++_index % _way.Count;
        }
    }

    public void SetWaipoint(Transform transform)
    {
        _wayPoint = transform;
    }

    private void MovingTowardsGoal(Collider2D collider)
    {
        if (collider != null)
            _target = collider.transform;
        else
            _target = _way[_index];
    }
}
