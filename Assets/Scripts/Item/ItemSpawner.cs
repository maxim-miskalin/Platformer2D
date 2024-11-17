using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _prefab;
    [SerializeField] private float _spawnTime = 5f;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new(0);
    }

    private void Start()
    {
        StartCoroutine(CreateItem());

        _wait = new(_spawnTime);
    }

    private void CreateNewCoin(Item item)
    {
        item.Destroyed += CreateNewCoin;
        StartCoroutine(CreateItem());
    }

    private IEnumerator CreateItem()
    {
        yield return _wait;

        Item item = Instantiate(_prefab, transform);
        item.Destroyed -= CreateNewCoin;
    }
}
