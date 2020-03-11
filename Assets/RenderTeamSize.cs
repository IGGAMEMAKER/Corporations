using Assets.Core;

public class RenderTeamSize : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        var max = Products.GetNecessaryAmountOfWorkers(SelectedCompany, Q);

        var flagshipId = Companies.GetPlayerFlagshipID(Q);

        bool isFlagship = SelectedCompany.company.Id == flagshipId;

        var workers = Teams.GetAmountOfWorkers(SelectedCompany, Q);

        if (!isFlagship)
            return workers.ToString();
            
        return workers + " / " + max;
    }
}
