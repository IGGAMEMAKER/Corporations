using Assets.Classes;
using Assets.Utils;
using System;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    // this will change for other company goals
    TeamResource GetResourceNecessity(GameEntity company)
    {
        return GetSegmentCost(company, UserType.Core);
        var stayInMarket = GetSegmentCost(company, UserType.Core);

        //// + 1 means that we want to become tech leaders
        var marketDiff = GetMarketDifference(company) + 1;

        return GetSegmentCost(company, UserType.Core) * marketDiff;
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

        var needPP = needResources.programmingPoints * ProductUtils.GetSegmentImprovementDuration(gameContext, company) / 30;


        if (production < needPP)
            return 1;

        var resource = company.companyResource.Resources;
        var currentPP = resource.programmingPoints;
        var overflowByPoints = currentPP > needPP * 2;

        if (overflowByPoints && production > needPP + Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER * 2)
            return -1;

        return 0;
    }
}
