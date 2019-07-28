using Assets.Utils;

public class MarketPhaseEstimatedTime : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, SelectedNiche);

        var timeRemaining = niche.nicheState.Duration * NicheUtils.GetNichePeriodDurationInMonths(niche);

        return $"{timeRemaining} months";
    }
}
