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

        if (isMe)
            text += " (YOU)";

        Text.text = text;

        if (isMe)
            Text.color = Visuals.GetColorFromString(Colors.COLOR_COMPANY_WHERE_I_AM_CEO);
        else
            Text.color = Color.white;
    }

    bool isMe
    {
        get
        {
            return SelectedHuman.human.Id == Me.human.Id;
        }
    }
}
