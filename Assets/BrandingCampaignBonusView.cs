using Assets.Utils;

public class BrandingCampaignBonusView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var brandingModifier = MarketingUtils.GetBrandingPowerGain(GameContext, MyProductEntity);

        GetComponent<ColoredValuePositiveOrNegative>().UpdateValue(brandingModifier);
    }
}
