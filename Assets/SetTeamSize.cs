using Assets.Utils;
using UnityEngine.UI;

public class SetTeamSize : ToggleButtonController
{
    public TeamStatus TeamSize;

    public override void Execute()
    {
        TeamUtils.Promote(SelectedCompany, TeamSize);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var money = CompanyEconomyUtils.GetAbstractTeamMaintenance(TeamSize);

        GetComponent<Button>().interactable = CompanyUtils.IsEnoughResources(SelectedCompany, new Assets.Classes.TeamResource(money));

        ToggleIsChosenComponent(SelectedCompany.team.TeamStatus == TeamSize);
    }
}
