using Assets.Utils;
using UnityEngine.UI;

public abstract class ParameterView : View
{
    internal Text Text;

    void PickComponents()
    {
        Text = GetComponent<Text>();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        PickComponents();

        Text.text = RenderValue();
    }

    public void Colorize(string color)
    {
        Text.color = Visuals.GetColorFromString(color);
    }

    public void Colorize(int value, int min, int max)
    {
        Text.color = Visuals.GetGradientColor(min, max, value);
    }

    public abstract string RenderValue();
}
