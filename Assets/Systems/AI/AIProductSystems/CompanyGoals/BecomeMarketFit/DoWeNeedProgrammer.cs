using Assets.Classes;
using Assets.Utils;
using System;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    // this will change for other company goals
    TeamResource GetResourceNecessity(GameEntity company)
    {
        return GetProductUpgradeCost(company);
    }


    // 1 - we need more programmers
    // 0 - we have good amount of programmers
    // -1 - we have more than we need
    int DoWeNeedProgrammer(GameEntity company)
    {
        var needResources = GetResourceNecessity(company);

        Print($"We need {needResources}", company);

        var change = GetResourceChange(company);
        var production = change.programmingPoints;

        var iterationLength = ProductUtils.GetSegmentImprovementDuration(gameContext, company);
        var requiredProduction = needResources.programmingPoints * 30 / iterationLength;


        if (production < requiredProduction)
            return 1;

        var resource = company.companyResource.Resources;
        var overflowByPoints = resource.programmingPoints > requiredProduction * 2;

        if (overflowByPoints && production > requiredProduction + Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER * 2)
            return -1;

        return 0;
    }
}
