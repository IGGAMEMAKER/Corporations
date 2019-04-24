using Assets.Utils;

public class PotentialLevelView : SimpleParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return ProductDevelopmentUtils.GetMarketRequirementsInNiche(GameContext, MyProduct.Niche) + "";
    }
}
