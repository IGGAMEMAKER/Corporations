using Assets.Core;

public class RenderTeamSize : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        var max = Products.GetNecessaryAmountOfWorkers(SelectedCompany, Q);

        return Teams.GetAmountOfWorkers(SelectedCompany, Q) + " / " + max;
    }
}
