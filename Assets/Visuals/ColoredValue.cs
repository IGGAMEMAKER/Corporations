using UnityEngine;
using UnityEngine.UI;

public enum MeasurementUnit
{
    Percent,
    Normal,
    Dollars
}

public enum CycleLength
{
    No,
    Monthly,
    Weekly,
    Daily
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
    public CycleLength cycleLength = CycleLength.No;

    Text Text;

    void Start()
    {
        Text = GetComponent<Text>();
    }

    void Update()
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

    string GetCycleString()
    {
        switch (cycleLength)
        {
            case CycleLength.Monthly: return "/month";
            case CycleLength.Daily: return "daily";
            case CycleLength.Weekly: return "/week";

            default: return "";
        }
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
        string text = "" + (Prettify ? ShowNDigitsAfterComma(value, DigitsAfterComma) : value);

        if (ShowSign && value > 0)
            text = "+" + text;

        string cycleString = GetCycleString();
        string measuringUnits = GetMeasuringUnitString(unit);
        

        return $"{text}{measuringUnits} {cycleString}";
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
