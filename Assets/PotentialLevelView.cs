using Assets.Utils;


// TODO REMOVE
public class PotentialLevelView : SimpleParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return "NOTHING. HAHA";
        //return ProductDevelopmentUtils.GetMarketRequirementsInNiche(GameContext, MyProduct.Niche) + "";
    }
}
