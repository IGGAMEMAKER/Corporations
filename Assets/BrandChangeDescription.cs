using Assets.Utils;

public class BrandChangeDescription : UpgradedParameterView
{
    public override string RenderHint()
    {
        return BrandPower.ToString();
    }

    public override string RenderValue()
    {
        return "Brand power: " + (int)SelectedCompany.branding.BrandPower;
    }

    Bonus<long> BrandPower => MarketingUtils.GetMonthlyBrandPowerChange(SelectedCompany, GameContext);
}
