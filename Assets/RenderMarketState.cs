using Assets.Utils;

public class RenderMarketState : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var state = State;
        var marketGrowthSpeed = Markets.GetMarketGrowth(state);

        var speed = marketGrowthSpeed;

        return Markets.GetMarketStateDescription(State) + " phase\n\nMarket Growth\n" + speed + "% / month";
    }

    MarketState State => Markets.GetMarketState(GameContext, SelectedNiche);
}
