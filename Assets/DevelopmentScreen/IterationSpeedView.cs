using Assets.Utils;

public class IterationSpeedView : ParameterView
{
    public override string RenderHint()
    {
        string hint = "Base Value: " + ProductDevelopmentUtils.BaseIterationTime(MyProductEntity) + " days";

        int modifier = ProductDevelopmentUtils.IterationTimeComplexityModifier(MyProductEntity) * 100;

        hint += "\n\n* Product Complexity Modifier: +" + modifier + "%";

        if (ProductDevelopmentUtils.IsCrunching(MyProductEntity))
            hint += "\n\n Is Crunching: -" + Constants.DEVELOPMENT_CRUNCH_BONUS + "%";

        return hint;
    }

    public override string RenderValue()
    {
        return ProductDevelopmentUtils.GetIterationTime(MyProductEntity) + " days";
    }
}
