using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarSlider : HealthView
{
    [SerializeField] protected Image Background;
    [SerializeField] protected Image Fill;

    protected Slider Slider;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    protected override void OnChangeValue(float current, float max)
    {
        Ratio = current / max;
        SetColorIndicator();
        Slider.value = Ratio;
    }

    protected override void ChangeColor(Color color)
    {
        Background.color = color;
        Fill.color = color;
    }
}
