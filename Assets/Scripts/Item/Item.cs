using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected float Value;

    public event Action<Item> Destroyed;

    public float Destroy()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
        return Value;
    }
}
