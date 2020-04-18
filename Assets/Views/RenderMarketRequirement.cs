using Assets.Core;

public class RenderMarketRequirement : ParameterView
{
    public override string RenderValue()
    {
        var niche = Markets.Get(Q, SelectedNiche);

        return Products.GetMarketRequirements(niche) + "LVL";
    }
}
