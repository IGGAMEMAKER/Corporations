using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// TODO extend ParameterView
public abstract class UpgradedParameterView2<T> : UpgradedParameterView
{
    public abstract T GetValue();
    public T value => GetValue();
}

public abstract class UpgradedParameterView : View
{
    internal Text Text;
    internal Hint Hint;

    void PickComponents()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        PickComponents();

        // Display value
        var value = RenderValue();


        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = value;

        if (GetComponent<TextMeshProUGUI>() != null)
            GetComponent<TextMeshProUGUI>().text = value;



        // Hints
        if (Hint != null)
        {
            string hint = RenderHint();

            Hint.SetHint(hint);
        }
    }

    public void Colorize(bool value, string colorNameTrue, string colorNameFalse) => Colorize(Visuals.GetColorFromString(value ? colorNameTrue : colorNameFalse));
    public void Colorize(int value, int min, int max) => Colorize(Visuals.GetGradientColor(min, max, value));
    public void Colorize(string color) => Colorize(Visuals.GetColorFromString(color));
    public void Colorize(Color color)
    {
        //Text.color = color;

        if (GetComponent<Text>() != null)
            GetComponent<Text>().color = color;

        if (GetComponent<TextMeshProUGUI>() != null)
            GetComponent<TextMeshProUGUI>().color = color;
    }



    public abstract string RenderValue();
    public abstract string RenderHint();
}
