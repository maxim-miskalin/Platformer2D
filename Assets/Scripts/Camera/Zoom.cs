using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Zoom : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 5f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _minValue = 2f;
    [SerializeField] private float _maxValue = 10f;

    private float _size;
    private Camera _camera;
    private string _mouseScrollWheel = "Mouse ScrollWheel";

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _size = (_minValue + _maxValue) / 2;
    }

    private void Update()
    {
        _size -= Input.GetAxis(_mouseScrollWheel) * _sensitivity;
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, Mathf.Clamp(_size, _minValue, _maxValue), Time.deltaTime * _speed);
    }
}
