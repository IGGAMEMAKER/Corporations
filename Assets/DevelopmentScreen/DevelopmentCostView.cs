using Assets.Utils;

public class DevelopmentCostView : SimpleParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return ProductDevelopmentUtils
            .GetDevelopmentCost(MyProductEntity, GameContext).programmingPoints.ToString();
    }
}
