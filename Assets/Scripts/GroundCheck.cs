using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundCheck : MonoBehaviour
{
    private int _numberOccurrences = 0;

    public bool IsGround => _numberOccurrences > 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            _numberOccurrences++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            _numberOccurrences--;
    }
}
