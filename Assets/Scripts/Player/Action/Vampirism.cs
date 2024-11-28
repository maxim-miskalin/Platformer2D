using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _radius = 6f;
    [SerializeField] private float _timeAction = 6f;
    [SerializeField] private float _delay = 0.5f;

    private Health _player;

    private float _timeWork;
    private float _timeCharging = 0;
    private bool _isWork = false;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    public event Action<float> StartedAbility;
    public event Action StoppedAbility;
    public event Action<float, float> ChangedValueTime;

    private void Awake()
    {
        _player = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _input.VampirismActivated += ActivateAbility;
    }

    private void OnDisable()
    {
        _input.VampirismActivated -= ActivateAbility;
    }

    private void Start()
    {
        _wait = new(_delay);
        _timeWork = _timeAction;
    }

    private void FixedUpdate()
    {
        if (!_isWork)
            _coroutine = null;

        if (_coroutine != null)
        {
            if (_timeWork >= 0)
                _timeWork -= Time.fixedDeltaTime;

            ChangedValueTime?.Invoke(_timeWork, _timeAction);
        }
        else
        {
            if (_timeCharging <= _timeAction)
                _timeCharging += Time.fixedDeltaTime;

            ChangedValueTime?.Invoke(_timeCharging, _timeAction);
        }
    }

    private void ActivateAbility()
    {
        if (_timeCharging >= _timeAction)
        {
            if (_coroutine == null)
            {
                _isWork = true;
                _coroutine = StartCoroutine(AbsorbHealth());
            }
        }
    }

    private IEnumerator AbsorbHealth()
    {
        StartedAbility?.Invoke(_radius);

        _timeCharging = 0;

        MoverSlime closestEnemy = null;

        while (_timeWork >= 0)
        {
            if (closestEnemy != null)
                if (Vector2.Distance(transform.position, closestEnemy.transform.position) > _radius)
                    closestEnemy = null;

            List<MoverSlime> enemies = new();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

            foreach (Collider2D collider in colliders)
                if (collider.TryGetComponent(out MoverSlime slime))
                    enemies.Add(slime);

            if (enemies.Count != 0)
            {
                closestEnemy = enemies[0];

                if (colliders.Length > 1)
                    for (int i = 1; i < enemies.Count; i++)
                        if (Vector2.Distance(transform.position, enemies[i].transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
                            closestEnemy = enemies[i];
            }
            else
            {
                closestEnemy = null;
            }

            if (closestEnemy != null)
            {
                if (closestEnemy.TryGetComponent(out Health health))
                {
                    if (health.CurrentValue > 0)
                    {
                        health.TakeDamage(_damage);
                        _player.RestoreValue(_damage);
                    }
                }
            }

            yield return _wait;
        }

        StoppedAbility?.Invoke();
        _timeWork = _timeAction;
        _isWork = false;
    }
}
