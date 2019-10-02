using Assets.Utils;
using System;

public class RenderNichePotential : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var potential = NicheUtils.GetMarketPotential(GameContext, SelectedNiche);

        if (potential == 0)
            return "???";

        var niche = NicheUtils.GetNicheEntity(GameContext, SelectedNiche);

        var min0 = NicheUtils.GetMarketPotentialMin(niche);
        var max0 = NicheUtils.GetMarketPotentialMax(niche);

        var min = Math.Min(min0, max0);
        var max = Math.Max(min0, max0);

        return Format.Money(min) + " ... " + Format.Money(max);
    }
}
