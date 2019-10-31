using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderPotentialMarketLeader : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var products = NicheUtils.GetProductsOnMarket(GameContext, SelectedNiche)
            .OrderByDescending(p => ProductUtils.GetInnovationChance(p, GameContext));

        if (products.Count() == 0)
            return "";

        var potentialLeader = products.First();

        var chances = ProductUtils.GetInnovationChance(potentialLeader, GameContext);

        return $"<b>{potentialLeader.company.Name}</b>\n\nInnovation chances: {chances}%";
    }
}
