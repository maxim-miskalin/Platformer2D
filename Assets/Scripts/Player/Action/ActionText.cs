using System;
using TMPro;
using UnityEngine;

public class ActionText : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;

    private float _ratio;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
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

        if (_ratio >= 1f)
            _text.text = "complete";
        else
            _text.text = Math.Round(current) + " s";
    }
}
