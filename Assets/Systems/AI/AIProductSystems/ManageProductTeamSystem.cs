using Assets.Core;
using System.Collections.Generic;
using System.Linq;

public partial class ManageProductTeamSystem : OnDateChange
{
    public ManageProductTeamSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetAIProducts(gameContext))
            ManageTeam(e);

        var playerProducts = Companies.GetPlayerRelatedProducts(gameContext);
        foreach (var e in playerProducts)
        {
            if (!Companies.IsPlayerFlagship(gameContext, e))
                ManageTeam(e);
        }
    }

    void HireRegularWorkers(GameEntity product)
    {
        var workerCost = Balance.SALARIES_PROGRAMMER;

        var needWorkers = Products.GetNecessaryAmountOfWorkers(product, gameContext);
        var haveWorkers = Teams.GetAmountOfWorkers(product, gameContext);

        if (Economy.IsCanMaintain(product, gameContext, workerCost) && haveWorkers < needWorkers)
            Teams.HireRegularWorker(product);
    }

    void HireManagers(GameEntity company)
    {
        // also usable for groups
        var managerCost = Balance.SALARIES_DIRECTOR;

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
