using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class RenderPlayerName : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var human = ScreenUtils.GetSelectedHuman(GameContext).human;

        var Text = GetComponent<Text>();
        var text = $"{human.Name} {human.Surname}";

        if (isMe)
            text += " (YOU)";

        Text.text = text;

        if (isMe)
            Text.color = Visuals.Color(VisualConstants.COLOR_COMPANY_WHERE_I_AM_CEO);
        else
            Text.color = Color.white;
    }

    bool isMe
    {
        get
        {
            return ScreenUtils.GetSelectedHuman(GameContext).human.Id == Me.human.Id;
        }
    }
}
