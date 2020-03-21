using Assets.Core;

public class RenderConceptLevel : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var company = SelectedCompany;

        //var level = Products.GetProductLevel(company);
        var max = Products.GetMarketRequirements(company, Q);

        //var description = Products.GetConceptStatus(company, Q);

        return max + "LVL";
    }
}
