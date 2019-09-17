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

        bool hasResources = CompanyUtils.IsEnoughResources(SelectedCompany, new Assets.Classes.TeamResource(money));

        GetComponent<Button>().interactable = hasResources;

        ToggleIsChosenComponent(SelectedCompany.team.TeamStatus == TeamSize);

        var text = "Company " + SelectedCompany.company.Name + " needs to have \n" + Visuals.Colorize(Format.MoneyToInteger(money), hasResources) + "\non their balance sheets to expand team";
        if (!hasResources)
            text += "\n They only have " + Format.MoneyToInteger(SelectedCompany.companyResource.Resources.money);

        GetComponent<Hint>().SetHint(text);
    }
}
