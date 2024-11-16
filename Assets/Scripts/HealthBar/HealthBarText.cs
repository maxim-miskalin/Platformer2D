using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthBarText : HealthView
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override void OnChangeValue(float current, float max)
    {
        Ratio = current / max;
        SetColorIndicator();
        _text.text = Math.Round(current) + "/" + Math.Round(max);
    }

    protected override void ChangeColor(Color color)
    {
        _text.color = color;
    }
}
