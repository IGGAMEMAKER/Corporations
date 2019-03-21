using Assets.Utils;

public class DevelopmentCostView : ParameterView
{
    public override string RenderHint()
    {
        string hint = "Base Cost: " + ProductDevelopmentUtils.BaseDevCost(myProductEntity);

        if (ProductDevelopmentUtils.IsInnovating(myProductEntity))
            hint += "\n Is Innovating: +" + Constants.DEVELOPMENT_INNOVATION_PENALTY + "%";

        return hint;
    }

    public override string RenderValue()
    {
        return ProductDevelopmentUtils.GetDevelopmentCost(myProductEntity).programmingPoints + "";
    }
}
