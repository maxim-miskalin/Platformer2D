using UnityEngine;

public class PocketSlime : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Died.AddListener(SpawnCoin);
    }

    private void OnDisable()
    {
        _health.Died.RemoveListener(SpawnCoin);
    }

    private void SpawnCoin(Health _)
    {
        Coin coin = Instantiate(_coin, transform.position, Quaternion.identity);
        coin.transform.SetParent(transform.parent.transform);
    }
}
