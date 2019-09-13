using Assets.Classes;
using Assets.Utils;
using System;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    void ManageSmallTeam(GameEntity company)
    {
        ManageProgrammers(company);
    }

    void ManageProductTeam(GameEntity company)
    {
        //DisableCrunches(company);

        ManageSmallTeam(company);

        ManageExpandedMarketingTeam(company);
    }

    void ManageProgrammers(GameEntity company)
    {
        var programmerNecessity = DoWeNeedProgrammer(company);

        switch (programmerNecessity)
        {
            case 1:
                HireWorker(company, WorkerRole.Programmer);
                break;
            case -1:
                FireWorkerByRole(company, WorkerRole.Programmer);
                break;
                // case 0: do nothing
        }
    }

    void ManageExpandedMarketingTeam(GameEntity company)
    {
        var doWeNeed = DoWeNeedMarketer(company);

        switch (doWeNeed)
        {
            case 1:
                HireWorker(company, WorkerRole.Marketer);
                break;
            case -1:
                FireWorkerByRole(company, WorkerRole.Marketer);
                break;
        }
    }

    void HireWorker(GameEntity company, WorkerRole workerRole)
    {
        if (IsCanAffordWorker(company, workerRole))
        {
            TeamUtils.HireWorker(company, workerRole);

            Print($"Hire {workerRole.ToString()}", company);
        }
    }

    bool FireWorkerByRole(GameEntity product, WorkerRole workerRole)
    {
        foreach (var w in product.team.Workers)
        {
            if (w.Value == workerRole)
            {
                TeamUtils.FireWorker(product, w.Key, gameContext);
                return true;
            }
        }

        return false;
    }
}
