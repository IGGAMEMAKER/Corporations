using Assets.Core;

public class RenderConceptLevel : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var company = SelectedCompany;

        var max = Products.GetMarketRequirements(company, Q);

        return max + "LVL";
    }
}
