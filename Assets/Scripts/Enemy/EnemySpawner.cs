using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MoverSlime[] _slimes;
    [SerializeField] private float _minRandomDelayValue = 5f;
    [SerializeField] private float _maxRandomDelayValue = 15f;

    private WaitForSeconds _wait;
    private bool _working = true;

    private void Start()
    {
        StartCoroutine(PlaySpawn());
    }

    private IEnumerator PlaySpawn()
    {
        while (_working)
        {
            _wait = new(Random.Range(_minRandomDelayValue, _maxRandomDelayValue));

            MoverSlime slime = Instantiate(GetRandomSlime(), transform.position, Quaternion.identity);
            slime.transform.SetParent(transform.parent.transform);
            slime.SetWaipoint(transform);

            yield return _wait;
        }
    }

    private MoverSlime GetRandomSlime()
    {
        int random = Random.Range(0, _slimes.Length - 1);

        return _slimes[random];
    }
}
