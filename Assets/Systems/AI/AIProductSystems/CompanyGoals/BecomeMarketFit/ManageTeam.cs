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
            case 1:
                if (IsCanAffordWorker(company, WorkerRole.Programmer))
                    HireWorker(company, WorkerRole.Programmer);
                break;
            case -1:
                FireWorkerByRole(company, WorkerRole.Programmer);
                break;
                // case 0: do nothing
        }
    }

    int GetMarketDifference(GameEntity company)
    {
        return ProductUtils.GetMarketDemand(company, gameContext, UserType.Core) - company.product.Concept;
    }
}
