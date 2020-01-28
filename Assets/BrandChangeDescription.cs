using Assets.Core;

public class BrandChangeDescription : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        if (Companies.IsExploredCompany(GameContext, SelectedCompany))
            return Marketing.GetBrandChange(SelectedCompany, GameContext).ToString();

        return "???";
    }
}
