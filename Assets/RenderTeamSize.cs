using Assets.Utils;
using UnityEngine.UI;

public class RenderTeamSize : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var size = TeamUtils.GetTeamSize(MyProductEntity);
        var max = TeamUtils.GetTeamMaxSize(MyProductEntity);

        GetComponent<Text>().text = $"{Visuals.Describe(max - size, size.ToString(), size.ToString(), size.ToString())} / {max}";
    }
}
