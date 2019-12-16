using Assets.Utils;

public class RenderMarketState : ParameterView
{
    public override string RenderValue()
    {
        var state = State;
        var speed = Markets.GetMarketGrowth(state);

        return Markets.GetMarketStateDescription(State) + " phase\n\nMarket Growth\n" + Visuals.Positive("+" + speed) + "% / month";
    }

    MarketState State => Markets.GetMarketState(GameContext, SelectedNiche);
}
