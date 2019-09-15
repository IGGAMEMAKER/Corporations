using Assets.Utils;

public class RenderConceptLevel : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return ProductUtils.GetProductLevel(SelectedCompany).ToString();
    }
}
