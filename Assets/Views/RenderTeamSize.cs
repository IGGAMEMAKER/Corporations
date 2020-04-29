using Assets.Core;

public class RenderTeamSize : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var company = Companies.GetFlagship(Q, MyCompany);

        var max = Products.GetNecessaryAmountOfWorkers(company, Q);
        var workers = Teams.GetTeamSize(company, Q);

        return workers + " / " + max;
    }
}
