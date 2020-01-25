using Assets.Core;

public class RenderMarketDemand : ParameterView
{
    public override string RenderValue()
    {
        var demand = MarketingUtils.GetClientFlow(GameContext, SelectedNiche);
        var audience = Markets.GetAudienceSize(GameContext, SelectedNiche);

        return $"{Format.Minify(audience)} ({Format.Minify(demand)} users)";
    }
}
