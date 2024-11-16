using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MoverSlime _greenSlime;
    [SerializeField] private MoverSlime _orangeSlime;
    [SerializeField] private float _minRandomDelayValue = 5f;
    [SerializeField] private float _maxRandomDelayValue = 15f;

    private WaitForSeconds _wait;
    private bool _working = true;
    private float _thresholdValue = 0.5f;

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
        float random = Random.Range(0f, 1f);

        if (random < _thresholdValue)
            return _greenSlime;
        else
            return _orangeSlime;
    }
}
