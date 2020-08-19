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
        }
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
        HireManagers(product);
    }
}
