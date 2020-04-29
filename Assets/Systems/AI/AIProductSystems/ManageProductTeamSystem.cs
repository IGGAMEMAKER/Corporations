using Assets.Core;
using System.Collections.Generic;
using System.Linq;

public partial class ManageProductTeamSystem : OnDateChange
{
    public ManageProductTeamSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var playerFlagshipId = Companies.GetPlayerFlagshipID(gameContext);

        foreach (var e in Companies.GetProductCompanies(gameContext))
        {
            if (e.company.Id != playerFlagshipId)
                ManageTeam(e);
            else
                HireRegularWorkers(e);
        }

        //var playerProducts = Companies.GetPlayerRelatedProducts(gameContext);
        //foreach (var e in playerProducts)
        //{
        //    if (!Companies.IsPlayerFlagship(gameContext, e))
        //        ManageTeam(e);
        //}
    }

    void ScaleTeam(GameEntity company)
    {
        var workers = Teams.GetTeamSize(company);
        var necessary = Products.GetNecessaryAmountOfWorkers(company, gameContext);

        company.team.Workers[WorkerRole.Programmer] = necessary;

        return;
        var need = necessary - workers;

        if (workers < necessary)
        {
            for (var i = 0; i < need; i++)
                Teams.HireRegularWorker(company);
        }
        else if (workers > necessary)
        {
            company.team.Workers[WorkerRole.Programmer] = necessary;
        }
    }

    void HireRegularWorkers(GameEntity product)
    {
        ScaleTeam(product);
        return;
        var workerCost = C.SALARIES_PROGRAMMER;

        var needWorkers = Products.GetNecessaryAmountOfWorkers(product, gameContext);
        var haveWorkers = Teams.GetTeamSize(product);

        if (Economy.IsCanMaintain(product, gameContext, workerCost) && haveWorkers < needWorkers)
            Teams.HireRegularWorker(product);
    }

    void HireManagers(GameEntity company)
    {
        // also usable for groups
        var managerCost = C.SALARIES_DIRECTOR;

        var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);
        var haveRoles = company.team.Managers.Values;

        var needRoles = roles.Where(r => !haveRoles.Contains(r));

        foreach (var r in needRoles)
        {
            if (Economy.IsCanMaintain(company, gameContext, managerCost))
                Teams.HireManager(company, gameContext, r);
        }
    }

    void ManageTeam(GameEntity product)
    {
        HireRegularWorkers(product);
        HireManagers(product);
    }
}
