using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public enum MeasurementUnit
{
    Percent,
    Normal,
    Dollars
}

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public abstract class ColoredValue : MonoBehaviour
{
    public float value;
    public bool ShowSign;

    [Space(20)]
    public bool Prettify = true;

    [Tooltip("Will show K instead of 1.000, M instead of 1.000.000 e.t.c")]
    public bool shorten;
    public int DigitsAfterComma = 0;

    public MeasurementUnit unit;

    void Start()
    {
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

    string GetMeasuringUnitString(MeasurementUnit unit)
    {
        switch (unit)
        {
            case MeasurementUnit.Normal: return "";
            case MeasurementUnit.Percent: return "%";
            case MeasurementUnit.Dollars: return "$";
            default: return "";
        }
    }

    string GetFormattedText()
    {
        string text = "";

        if (Prettify)
            text = ShowNDigitsAfterComma(value, DigitsAfterComma).ToString();

        if (shorten)
            text = Format.Minify(value);
        else
            text = value.ToString();

        if (ShowSign && value > 0)
            text = "+" + text;

        string measuringUnits = GetMeasuringUnitString(unit);
        

        return $"{text}{measuringUnits}";
    }

    void Render()
    {
        Text Text = GetComponent<Text>();

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
