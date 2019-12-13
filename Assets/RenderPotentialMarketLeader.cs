using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RenderPotentialMarketLeader : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        //var products = NicheUtils.GetProductsOnMarket(GameContext, SelectedNiche)
        //    .OrderByDescending(p => ProductUtils.GetInnovationChance(p, GameContext));

        //if (products.Count() == 0)
        //    return "";

        //var potentialLeader = products.First();
        var potentialLeader = Markets.GetPotentialMarketLeader(GameContext, SelectedNiche);

        if (potentialLeader == null)
            return "";

        var chances = Products.GetInnovationChance(potentialLeader, GameContext);

        var isRelatedToPlayer = Companies.IsCompanyRelatedToPlayer(GameContext, potentialLeader);

        var colorName = isRelatedToPlayer ? VisualConstants.COLOR_CONTROL : VisualConstants.COLOR_CONTROL_NO;

        return $"<b>{Visuals.Colorize(potentialLeader.company.Name, colorName)}</b>"; // \n\nInnovation chances: {chances}%";
    }
}
