using Assets.Core;

public class RenderConceptLevel : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var level = Products.GetProductLevel(SelectedCompany);
        var max = Products.GetMarketRequirements(SelectedCompany, Q);

        return Visuals.Gradient(0, max, level) + " / " + max;
    }
}
