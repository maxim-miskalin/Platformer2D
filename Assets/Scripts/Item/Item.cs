using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected float Value;

    public UnityEvent<Item> Disappear;

    public abstract float ToDestroyed();
}
