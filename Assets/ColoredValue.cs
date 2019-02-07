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

    public MeasurementUnit unit;

    Text Text;

    void Start()
    {
        Text = GetComponent<Text>();

        Render();
    }

    public abstract Color GetColor();

    string GetFormattedText()
    {
        string text = value.ToString();

        if (ShowSign && value > 0)
            text = "+" + value;

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

    //public void Update()
    //{
    //    Render();
    //}
}
