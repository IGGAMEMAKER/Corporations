using Assets.Utils;
using System.Linq;

public class RenderMarketState : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "Companies get new clients due to their brand powers";
    }

    string GetGrowthDescription (GameEntity p)
    {
        var growth = MarketingUtils.GetBrandBasedAudienceGrowth(p, GameContext);

        return $"{p.company.Name} (): +{Format.Minify(growth)} users";
    }

    public override string RenderValue()
    {
        var state = Markets.GetMarketState(GameContext, SelectedNiche);
        var speed = Markets.GetMarketGrowth(state);


        var flow = MarketingUtils.GetClientFlow(GameContext, SelectedNiche);

        return Visuals.Positive("+" + speed) + $"% / month\n\nMarket will get {Format.MinifyToInteger(flow)} new users this month";
    }
}
