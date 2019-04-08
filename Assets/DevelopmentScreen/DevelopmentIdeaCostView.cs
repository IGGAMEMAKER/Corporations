using Assets.Utils;

public class DevelopmentIdeaCostView : ParameterView
{
    public override string RenderHint()
    {
        string hint = "Base Cost: " + ProductDevelopmentUtils.BaseIdeaCost(MyProductEntity);

        if (ProductDevelopmentUtils.IsInnovating(MyProductEntity))
            hint += "\n Is Innovating: +" + Constants.DEVELOPMENT_INNOVATION_PENALTY + "%";

        return hint;
    }

    public override string RenderValue()
    {
        return ProductDevelopmentUtils.GetDevelopmentCost(MyProductEntity).ideaPoints + "";
    }
}
