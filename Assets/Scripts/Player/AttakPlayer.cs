using UnityEngine;

[RequireComponent(typeof(MoverPlayer))]
public class AttakPlayer : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Fireball _prefab;
    [SerializeField, Min(1f)] private float _distanceSpawn = 1f;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _input.Attacked += Attack;
    }

    private void OnDisable()
    {
        _input.Attacked -= Attack;
    }

    private void Attack()
    {
        Vector2 direction = _camera.ScreenPointToRay(Input.mousePosition).origin - transform.position;
        float directionMagnitude = direction.magnitude;
        Vector2 spawn = (_distanceSpawn / directionMagnitude) * direction;
        Fireball fireball = Instantiate(_prefab, (Vector2)transform.position + spawn, Quaternion.identity);
        fireball.transform.SetParent(transform.parent);
        fireball.SetDirection(direction / directionMagnitude);
    }
}
