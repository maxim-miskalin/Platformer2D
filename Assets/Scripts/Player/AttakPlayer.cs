using System.Net;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MoverPlayer))]
public class AttakPlayer : MonoBehaviour
{
    [SerializeField] private Fireball _prefab;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField, Min(1f)] private float _distanceSpawn = 1f;

    private Collider2D _collider;
    private MoverPlayer _player;
    private Camera _camera;

    private void Awake()
    {
        _player = GetComponent<MoverPlayer>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _player.Attacked.AddListener(Attack);
    }

    private void OnDisable()
    {
        _player.Attacked.RemoveListener(Attack);
    }

    private void Attack()
    {
        Vector2 direction = _camera.ScreenPointToRay(Input.mousePosition).origin - transform.position;
        float directionMagnitude = direction.magnitude;
        Vector2 spawn = (_distanceSpawn / directionMagnitude) * direction;
        Fireball fireball = Instantiate(_prefab, (Vector2)transform.position + spawn, Quaternion.identity);
        fireball.SetDirection(direction / directionMagnitude);
    }
}
