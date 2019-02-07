using UnityEngine;
using UnityEngine.UI;

public enum MeasurementUnit
{
    Percent,
    Normal
}

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public abstract class ColoredValue : MonoBehaviour
{
    public float value;
    public bool ShowSign;

    [Space(20)]
    public bool Prettify = true;
    public int DigitsAfterComma = 0;

    public MeasurementUnit unit;

    Text Text;

    void Start()
    {
        Text = GetComponent<Text>();

        Render();
    }

    public abstract Color GetColor();

    float ShowNDigitsAfterComma(float val, float digits)
    {
        if (digits <= 0)
            return Mathf.Floor(val);

        float multiplier = Mathf.Pow(10, digits);

        return (Mathf.Floor(val * multiplier)) / multiplier;
    }

    string GetFormattedText()
    {
        string text = "" + (Prettify ? ShowNDigitsAfterComma(value, DigitsAfterComma) : value);

        if (ShowSign && value > 0)
            text = "+" + text;

        return text + "%";
    }

    void Render()
    {
        Color color = GetColor();
        Text.color = color;

        string text = GetFormattedText();
        Text.text = text;
    }

    public void UpdateValue(float val)
    {
        value = val;

        Render();
    }
}
