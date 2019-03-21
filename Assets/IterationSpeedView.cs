using Assets.Utils;

public class IterationSpeedView : ParameterView
{
    public override string RenderHint()
    {
        string hint = "Base Value: " + ProductDevelopmentUtils.BaseIterationTime(myProductEntity) + " days";

        hint += "\n Product Complexity Modifier: +" + ProductDevelopmentUtils.IterationTimeComplexityModifier(myProductEntity) + "%";

        if (ProductDevelopmentUtils.IsCrunching(myProductEntity))
            hint += "\n Is Crunching: -" + Constants.DEVELOPMENT_CRUNCH_BONUS + "%";

        return hint;
    }

    public override string RenderValue()
    {
        return ProductDevelopmentUtils.GetIterationTime(myProductEntity) + " days";
    }
}
