using UnityEngine;

public class HideUpgradeTeamButton : View
{
    public GameObject UpgradeTeamButton;

    public override void ViewRender()
    {
        base.ViewRender();

        UpgradeTeamButton.SetActive(MyCompany.team.TeamStatus < TeamStatus.Department);
    }
}
