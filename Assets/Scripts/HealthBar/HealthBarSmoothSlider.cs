using System.Collections;
using UnityEngine;

public class HealthBarSmoothSlider : HealthBarSlider
{
    [SerializeField] private float _delay = 0.01f;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Start()
    {
        _wait = new(_delay);
    }

    protected override void OnChangeValue(float current, float max)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        Ratio = current / max;
        SetColorIndicator();
        _coroutine = StartCoroutine(MoveSmoothSlider());
    }

    private IEnumerator MoveSmoothSlider()
    {
        float elapsedTime = 0;

        while (Slider.value != Ratio)
        {
            Slider.value = Mathf.Lerp(Slider.value, Ratio, elapsedTime);
            elapsedTime += Time.deltaTime;

            yield return _wait;
        }
    }
}
