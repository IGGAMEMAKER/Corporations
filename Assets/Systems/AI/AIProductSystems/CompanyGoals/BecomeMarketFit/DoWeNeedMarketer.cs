using Assets.Classes;
using Assets.Utils;
using System;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    // this will change for other company goals
    long GetRequiredMarketingResources(GameEntity company)
    {
        return CompanyEconomyUtils.GetAverageMarketingMaintenance(company, gameContext);
    }

    int DoWeNeedMarketer(GameEntity company)
    {
        var change = GetResourceChange(company);

        var need = GetRequiredMarketingResources(company);
        var production = change.salesPoints;

        //Debug.Log($"Company {company.company.Name} >>> Required SP: {need}, Have SP: {production}");

        if (production < need)
            return 1;

        if (production > need + Constants.DEVELOPMENT_PRODUCTION_MARKETER * 3)
            return -1;

        return 0;
    }
}
