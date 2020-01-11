using Assets.Core;

public class RenderFeatures : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var freeFeatures = Products.GetFreeImprovements(product);

        return freeFeatures.ToString();
    }
}
