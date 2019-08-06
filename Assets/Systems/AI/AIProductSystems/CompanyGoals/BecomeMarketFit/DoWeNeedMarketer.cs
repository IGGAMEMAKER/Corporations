using Assets.Classes;
using Assets.Utils;
using System;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    // this will change for other company goals
    long GetRequiredMarketingResources(GameEntity company)
    {
        var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);

        return NicheUtils.GetBaseMarketingMaintenance(niche).salesPoints;
    }

    int DoWeNeedMarketer(GameEntity company)
    {
        var change = GetResourceChange(company);

        var need = GetRequiredMarketingResources(company);
        var production = change.salesPoints;

        //Debug.Log($"Company {company.company.Name} >>> Required SP: {need}, Have SP: {production}");
        //Print($"We need {need}, but produce {production}", company);


        if (production < need * 1.2f)
            return 1;

        //if (production > need + Constants.DEVELOPMENT_PRODUCTION_MARKETER * 3)
        //    return -1;

        return 0;
    }
}
