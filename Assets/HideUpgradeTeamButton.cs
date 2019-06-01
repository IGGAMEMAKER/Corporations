using UnityEngine;

public class HideUpgradeTeamButton : View
{
    public GameObject UpgradeTeamButton;

    public override void ViewRender()
    {
        base.ViewRender();

        UpgradeTeamButton.SetActive(MyProductEntity.team.TeamStatus < TeamStatus.Department);
    }
}
