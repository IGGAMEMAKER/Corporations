using Assets.Utils;
using UnityEngine.UI;

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

        Text.text = RenderValue();

        string hint = RenderHint();

        if (hint.Length > 0)
            Hint.SetHint(hint);
    }

    public void Colorize(string color)
    {
        Text.color = Visuals.Color(color);
    }

    public abstract string RenderValue();
    public abstract string RenderHint();
}
