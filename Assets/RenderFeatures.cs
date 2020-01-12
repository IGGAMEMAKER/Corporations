using Assets.Core;

public class RenderFeatures : ParameterView
{
    public override string RenderValue()
    {
        return Products.GetFreeImprovements(SelectedCompany).ToString();
    }
}
