using Assets.Utils;
using UnityEngine.UI;

public class RenderTeamSize : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var size = TeamUtils.GetTeamSize(SelectedCompany);
        var max = TeamUtils.GetTeamMaxSize(SelectedCompany);

        GetComponent<Text>().text = $"{Visuals.Describe(max - size, size.ToString(), size.ToString(), size.ToString())} / {max}";
    }
}
