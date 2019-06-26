using Assets.Utils;

public class DevelopmentCostView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var cost = ProductDevelopmentUtils.GetDevelopmentCost(MyProductEntity, GameContext).programmingPoints;

        return Format.Minify(cost);
    }
}
