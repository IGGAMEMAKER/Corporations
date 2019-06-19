using Assets.Utils;

public class DevelopmentIdeaCostView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var cost = ProductDevelopmentUtils.GetDevelopmentCost(MyProductEntity, GameContext).ideaPoints;

        return Format.Shorten(cost);
    }
}
