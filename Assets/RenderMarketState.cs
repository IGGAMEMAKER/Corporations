using Assets.Utils;

public class RenderMarketState : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        return Markets.GetMarketStateDescription(State) + " phase";
    }

    NicheState State => Markets.GetMarketState(GameContext, SelectedNiche);
}
