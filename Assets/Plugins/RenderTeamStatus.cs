using UnityEngine.UI;

public class RenderTeamStatus : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<Text>().text = SelectedCompany.team.TeamStatus.ToString();
    }
}
