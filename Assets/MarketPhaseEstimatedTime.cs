using Assets.Utils;

public class MarketPhaseEstimatedTime : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var niche = NicheUtils.GetNiche(GameContext, SelectedNiche);

        var timeRemaining = niche.nicheState.Duration; // * NicheUtils.GetNichePeriodDurationInMonths(niche);

        var years = timeRemaining / 12;

        if (years > 1)
            return $"{years}+ years";

        return $"{timeRemaining} months";
    }
}
