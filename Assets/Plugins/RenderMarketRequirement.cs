using Assets.Utils;

public class RenderMarketRequirement : ParameterView
{
    public override string RenderValue()
    {
        var niche = Markets.GetNiche(GameContext, SelectedNiche);

        return Products.GetMarketRequirements(niche) + "LVL";
    }
}
