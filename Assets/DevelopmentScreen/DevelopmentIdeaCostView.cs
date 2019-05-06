using Assets.Utils;

public class DevelopmentIdeaCostView : SimpleParameterView
{
    public override string RenderHint()
    {
        return "";
        string hint = "Base Cost: " + ProductDevelopmentUtils.BaseIdeaCost(MyProductEntity);

        //if (ProductDevelopmentUtils.IsInnovating(MyProductEntity, GameContext))
        //    hint += "\n Is Innovating: +" + Constants.DEVELOPMENT_INNOVATION_PENALTY + "%";

        return hint;
    }

    public override string RenderValue()
    {
        return ProductDevelopmentUtils.GetDevelopmentCost(MyProductEntity, GameContext).ideaPoints + "";
    }
}
