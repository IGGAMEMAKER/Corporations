using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderPlayerName : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var Text = GetComponent<Text>();
        var text = Humans.GetFullName(SelectedHuman);

        if (IsMe)
            text += " (YOU)";

        Text.text = $"{text} #{SelectedHuman.creationIndex}";

        if (IsMe)
            Text.color = Visuals.GetColorFromString(Colors.COLOR_COMPANY_WHERE_I_AM_CEO);
        else
            Text.color = Color.white;
    }

    bool IsMe => SelectedHuman.human.Id == Hero.human.Id;
}
