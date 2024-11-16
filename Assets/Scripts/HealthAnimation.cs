using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(Mover))]
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(Health))]
public class HealthAnimation : MonoBehaviour
{
    [SerializeField] private HealthView _healthBar;
    [SerializeField] private float _delayOfDisappearance = 5f;
    [SerializeField] private Color _damageColor = Color.red;
    [SerializeField] private Color _treatmentColor = Color.green;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private Health _health;

    private Mover _mover;

    private float _pastValue = 0;
    private Color _defaultColor;
    private Color _deathColor;
    private float _deathColorAlfa = 0.5f;
    private WaitForSeconds _waitDie;
    private WaitForSeconds _waitColor;
    private Coroutine _coroutine;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();

        _defaultColor = _renderer.color;
    }

    private void OnEnable()
    {
        _health.ValueChanged.AddListener(CheckHealth);
        _health.Died.AddListener(Die);
    }

    private void OnDisable()
    {
        _health.ValueChanged.RemoveListener(CheckHealth);
        _health.Died.RemoveListener(Die);
    }

    private void Start()
    {
        _waitDie = new(_delayOfDisappearance);
        _waitColor = new(0.1f);
        _deathColor = _renderer.color;
        _deathColor.a = _deathColorAlfa;
    }

    private void CheckHealth(float current, float max)
    {

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (current == max)
            _coroutine = StartCoroutine(ChangeColor(_defaultColor));
        else if (current > _pastValue)
            _coroutine = StartCoroutine(ChangeColor(_treatmentColor));
        else if (current < _pastValue)
            _coroutine = StartCoroutine(ChangeColor(_damageColor));

        _pastValue = current;
    }

    private void Die(Health _)
    {
        StopCoroutine(_coroutine);

        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        _mover.enabled = false;
        _animator.enabled = false;
        _rigidbody.simulated = false;

        if (_healthBar != null)
            _healthBar.gameObject.SetActive(false);

        transform.rotation = Quaternion.Euler(0, 0, 90f);
        _renderer.color = _deathColor;
        _collider.enabled = false;

        yield return _waitDie;

        Destroy(gameObject);
    }

    private IEnumerator ChangeColor(Color color)
    {
        _renderer.color = color;
        yield return _waitColor;
        _renderer.color = _defaultColor;
    }
}
