using Assets.Utils;
using System;
using System.Linq;

public class RenderNichePotential : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var products = NicheUtils.GetProductsOnMarket(GameContext, SelectedNiche);


        var clients = products.Sum(p => MarketingUtils.GetClients(p));

        var flow = MarketingUtils.GetClientFlow(GameContext, SelectedNiche);

        return $"{Format.Minify(clients)} users\n\n+{Format.Minify(flow)} this month";


        var potential = NicheUtils.GetMarketPotential(GameContext, SelectedNiche);

        if (potential == 0)
            return "???";

        var niche = NicheUtils.GetNiche(GameContext, SelectedNiche);

        var min0 = NicheUtils.GetMarketPotentialMin(niche);
        var max0 = NicheUtils.GetMarketPotentialMax(niche);

        var min = Math.Min(min0, max0);
        var max = Math.Max(min0, max0);

        return Format.Money(min) + " ... " + Format.Money(max);
    }
}
