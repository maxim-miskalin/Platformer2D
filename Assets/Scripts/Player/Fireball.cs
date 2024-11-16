using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Fireball : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _damage = 15f;
    [SerializeField] private float _timeLife = 5f;

    private Vector2 _direction;

    private void Start()
    {
        Destroy(gameObject, _timeLife);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Health health))
            health.TakeDamage(_damage);

        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * (Vector3)_direction;
    }

    public void SetDirection(Vector2 direction) => _direction = direction;
}
