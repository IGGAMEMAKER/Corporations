using Assets.Utils;

public class RenderConceptLevel : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var level = Products.GetProductLevel(SelectedCompany);
        var max = Products.GetMarketRequirements(SelectedCompany, GameContext);

        return Visuals.Gradient(0, max, level) + " / " + max;
    }
}
