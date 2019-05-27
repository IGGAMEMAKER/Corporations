using Assets.Utils;
using UnityEngine.UI;

public class RenderPlayerName : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var human = ScreenUtils.GetSelectedHuman(GameContext).human;

        GetComponent<Text>().text = human.Name + " " + human.Surname;
    }
}
