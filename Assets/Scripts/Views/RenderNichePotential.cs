using Assets.Core;
using System;
using System.Linq;

public class RenderNichePotential : ParameterView
{
    public override string RenderValue()
    {
        return "";
        var clients = Markets.GetAudienceSize(Q, SelectedNiche);

        var flow = Marketing.GetClientFlow(Q, SelectedNiche);

        return $"{Format.Minify(clients)} users\n\n+{Format.Minify(flow)} this month";


        var potential = Markets.GetMarketPotential(Q, SelectedNiche);

        if (potential == 0)
            return "???";

        var niche = Markets.GetNiche(Q, SelectedNiche);

        var min0 = Markets.GetMarketPotentialMin(niche);
        var max0 = Markets.GetMarketPotentialMax(niche);

        var min = Math.Min(min0, max0);
        var max = Math.Max(min0, max0);

        return Format.Money(min) + " ... " + Format.Money(max);
    }
}
