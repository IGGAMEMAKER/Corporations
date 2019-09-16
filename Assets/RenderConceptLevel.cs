using Assets.Utils;

public class RenderConceptLevel : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var level = ProductUtils.GetProductLevel(SelectedCompany);
        var max = ProductUtils.GetMarketDemand(SelectedCompany, GameContext);

        return Visuals.Gradient(max - 1, max, level) + " / " + max;
    }
}
