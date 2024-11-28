using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private string _horizontalInput = "Horizontal";
    [SerializeField] private string _jumpInput = "Jump";
    [SerializeField] private KeyCode _sitDownInput = KeyCode.LeftShift;
    [SerializeField] private KeyCode _vampirismInput = KeyCode.V;


    public event Action Attacked;
    public event Action Jumped;
    public event Action<float> Moved;
    public event Action<bool> DownSquatted;
    public event Action VampirismActivated;

    private void Update()
    {
        if (Input.GetButtonDown(_jumpInput))
            Jumped?.Invoke();

        Moved?.Invoke(Input.GetAxis(_horizontalInput));

        if (Input.GetMouseButtonDown(0))
            Attacked.Invoke();

        if (Input.GetKey(_sitDownInput))
            DownSquatted?.Invoke(true);
        else
            DownSquatted?.Invoke(false);

        if (Input.GetKeyDown(_vampirismInput))
            VampirismActivated?.Invoke();
    }
}
