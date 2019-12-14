using Assets.Utils;

public class BrandChangeDescription : UpgradedParameterView
{
    public override string RenderHint()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        return BrandPower.ToString();
    }

    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        return "Brand power: " + (int)SelectedCompany.branding.BrandPower;
    }

    Bonus<long> BrandPower => MarketingUtils.GetMonthlyBrandPowerChange(SelectedCompany, GameContext);
}
