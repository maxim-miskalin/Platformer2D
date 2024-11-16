using TMPro;
using UnityEngine;

public class PocketView : MonoBehaviour
{
    [SerializeField] private Pocket _pocket;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _pocket.ValueChanged.AddListener(DisplayText);
    }

    private void OnDisable()
    {
        _pocket.ValueChanged.RemoveListener(DisplayText);
    }

    private void DisplayText(float value)
    {
        _text.text = Mathf.Round(value) + " coin";
    }
}
