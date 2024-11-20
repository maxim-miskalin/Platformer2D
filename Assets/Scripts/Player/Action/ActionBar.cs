//using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ActionBar : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Image _background;
    [SerializeField] private Image _fill;
    [Header("Color of bar value")]
    [SerializeField] private Color _colorIndicator = new(1f, 1f, 1f);
    [SerializeField] private float _lightCriticalValueFraction = 0.3f;
    [SerializeField] private Color _colorIndicatorLightCriticalValue = new(1f, 1f, 0);
    [SerializeField] private float _heavyCriticalValueFraction = 0.01f;
    [SerializeField] private Color _colorIndicatorHeavyCriticalValue = new(1, 0, 0);

    private Slider _slider;
    private float _ratio;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _vampirism.ChangedValueTime += OnChangeValue;
    }

    private void OnDisable()
    {
        _vampirism.ChangedValueTime -= OnChangeValue;
    }

    private void OnChangeValue(float current, float max)
    {
        _ratio = current / max;
        SetColorIndicator();
        _slider.value = _ratio;
    }

    protected void SetColorIndicator()
    {
        if (_ratio > _lightCriticalValueFraction)
            ChangeColor(_colorIndicator);
        else if (_ratio <= _lightCriticalValueFraction && _ratio > _heavyCriticalValueFraction)
            ChangeColor(GetColor());
        else if (_ratio <= _heavyCriticalValueFraction)
            ChangeColor(GetColor());
    }

    private void ChangeColor(Color color)
    {
        _background.color = color;
        _fill.color = color;
    }

    private Color GetColor()
    {
        float value = (_ratio - _heavyCriticalValueFraction) / _lightCriticalValueFraction;

        Color.RGBToHSV(_colorIndicatorLightCriticalValue, out float LightValueH, out float LightValueS, out float LightValueV);
        Color.RGBToHSV(_colorIndicatorHeavyCriticalValue, out float HeavyValueH, out float HeavyValueS, out float HeavyValueV);

        float currentH = (LightValueH + HeavyValueH) / 2;
        float currentS = (LightValueS + HeavyValueS) / 2;
        float currentV = (LightValueV + HeavyValueV) / 2;

        if (LightValueH != HeavyValueH)
            currentH = (LightValueH + HeavyValueH) * value;
        if (LightValueS != HeavyValueS)
            currentS = (LightValueS + HeavyValueS) * value;
        if (LightValueV != HeavyValueV)
            currentV = (LightValueV + HeavyValueV) * value;

        Color color = Color.HSVToRGB(currentH, currentS, currentV);

        return color;
    }
}
