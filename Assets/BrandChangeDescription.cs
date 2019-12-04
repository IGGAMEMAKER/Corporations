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

    Bonus<float> BrandPower => MarketingUtils.GetMonthlyBrandPowerChange(SelectedCompany, GameContext);
}
