using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CheckGround : MonoBehaviour
{
    public bool IsGround { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            IsGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            IsGround = false;
    }
}
