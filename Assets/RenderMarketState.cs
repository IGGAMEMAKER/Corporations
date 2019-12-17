using Assets.Utils;

public class RenderMarketState : ParameterView
{
    public override string RenderValue()
    {
        var state = Markets.GetMarketState(GameContext, SelectedNiche);
        var speed = Markets.GetMarketGrowth(state);

        return "Market Growth\n" + Visuals.Positive("+" + speed) + "% / month";
        //return Markets.GetMarketStateDescription(State) + " phase\n\nMarket Growth\n" + Visuals.Positive("+" + speed) + "% / month";
    }
}
