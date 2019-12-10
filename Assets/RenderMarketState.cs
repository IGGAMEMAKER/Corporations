using Assets.Utils;

public class RenderMarketState : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Companies.GetMarketStateDescription(State) + " phase";
    }

    NicheState State => Markets.GetMarketState(GameContext, SelectedNiche);
}
