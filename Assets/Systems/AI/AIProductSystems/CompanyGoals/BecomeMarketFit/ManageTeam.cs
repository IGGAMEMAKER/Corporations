using Assets.Classes;
using Assets.Utils;
using System;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    void ManageSmallTeam(GameEntity company)
    {
        if (TeamUtils.IsWillOverextendTeam(company))
        {
            TeamUtils.Promote(company);
            return;
        }

        ManageProgrammers(company);
    }

    void ManageProgrammers(GameEntity company)
    {
        var programmerNecessity = DoWeNeedProgrammer(company);

        switch (programmerNecessity)
        {
            case 1: HireWorker(company, WorkerRole.Programmer); break;
            case -1: FireWorkerByRole(company, WorkerRole.Programmer); break;
                // case 0: do nothing
        }
    }

    int GetMarketDifference(GameEntity company)
    {
        return ProductUtils.GetMarketDemand(company, gameContext, UserType.Core) - company.product.Concept;
    }

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

        var resource = company.companyResource.Resources;
        var change = GetResourceChange(company);

        var production = change.programmingPoints;

        var needPP = needResources.programmingPoints * ProductUtils.GetSegmentImprovementDuration(gameContext, company) / 30;
        var currentPP = resource.programmingPoints;

        var overflowByPoints = currentPP > needPP * 2;

        if (production < needPP)
            return 1;

        if (overflowByPoints && production > needPP + Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER)
            return -1;

        return 0;
    }
}
