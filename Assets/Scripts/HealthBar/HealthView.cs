using UnityEngine;
using UnityEngine.UIElements;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [Header("Color of bar value")]
    [SerializeField] protected Color ColorIndicator = new(1f, 1f, 1f);
    [SerializeField] protected float LightCriticalValueFraction = 0.3f;
    [SerializeField] protected Color ColorIndicatorLightCriticalValue = new(1f, 1f, 0);
    [SerializeField] protected float HeavyCriticalValueFraction = 0.01f;
    [SerializeField] protected Color ColorIndicatorHeavyCriticalValue = new(1, 0, 0);

    protected float Ratio;

    private void OnEnable()
    {
        Health.ValueChanged.AddListener(OnChangeValue);
    }

    private void OnDisable()
    {
        Health.ValueChanged.RemoveListener(OnChangeValue);
    }

    protected void SetColorIndicator()
    {
        if (Ratio > LightCriticalValueFraction)
            ChangeColor(ColorIndicator);
        else if (Ratio <= LightCriticalValueFraction && Ratio > HeavyCriticalValueFraction)
            ChangeColor(GetColor());
        else if (Ratio <= HeavyCriticalValueFraction)
            ChangeColor(GetColor());
    }

    protected abstract void OnChangeValue(float current, float max);

    protected abstract void ChangeColor(Color color);

    private Color GetColor()
    {
        float value = (Ratio - HeavyCriticalValueFraction) / LightCriticalValueFraction;

        Color.RGBToHSV(ColorIndicatorLightCriticalValue, out float LightValueH, out float LightValueS, out float LightValueV);
        Color.RGBToHSV(ColorIndicatorHeavyCriticalValue, out float HeavyValueH, out float HeavyValueS, out float HeavyValueV);

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
