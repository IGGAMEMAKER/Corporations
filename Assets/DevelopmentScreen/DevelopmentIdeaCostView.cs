using Assets.Utils;

public class DevelopmentIdeaCostView : SimpleParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return ProductDevelopmentUtils
            .GetDevelopmentCost(MyProductEntity, GameContext).ideaPoints.ToString();
    }
}
