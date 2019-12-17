using Assets.Utils;

public class BrandChangeDescription : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        if (Companies.IsExploredCompany(GameContext, SelectedCompany))
            return MarketingUtils.GetBrandChange(SelectedCompany, GameContext).ToString();

        return "???";
    }
}
