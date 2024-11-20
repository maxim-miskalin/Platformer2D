using System.Collections;
using UnityEngine;

public class ScopeAction : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private float _delay = 0.01f;
    [SerializeField] private float _speedAnimation = 0.5f;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        _wait = new(_delay);
    }

    private void OnEnable()
    {
        _vampirism.StartedAbility += StartAnimation;
        _vampirism.StoppedAbility += StopAnimation;
    }

    private void OnDisable()
    {
        _vampirism.StartedAbility -= StartAnimation;
        _vampirism.StoppedAbility -= StopAnimation;
    }

    private void StartAnimation(float radius)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Resize(Vector3.one * (radius * 2)));
    }

    private void StopAnimation()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Resize(Vector3.zero));
    }

    private IEnumerator Resize(Vector3 size)
    {
        while (transform.localScale != size)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, size, _speedAnimation);// * _delay);

            yield return _wait;
        }
    }
}
