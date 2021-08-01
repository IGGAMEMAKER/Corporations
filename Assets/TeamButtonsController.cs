using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamButtonsController : View
{
    public EventView AddNewTeam;
    public EventView DetachTeam;
    public EventView PromoteTeam;

    public EventView Hire;
    public EventView ManagerFocus;


    public override void ViewRender()
    {
        var progress = 15;

        var product = Flagship;

        var core = product.team.Teams[0];

        var mp = product.companyResource.Resources.managerPoints;
        var needToCreate = Teams.GetPromotionCost(TeamRank.Solo);

        progress = Mathf.Clamp(mp, 0, needToCreate) * 100 / needToCreate;


        var team = product.team.Teams[SelectedTeam];


        bool hasLeadManager = Teams.HasMainManagerInTeam(team);


        bool isCoreTeam = SelectedTeam == 0;

        bool isUpToManagerLimit = Teams.GetDirectManagementCostOfTeam(core, product, Q).Sum() < 1.2f;
        bool grownUp = isUpToManagerLimit; // Teams.HasRole(WorkerRole.Marketer, core) && Teams.HasRole(WorkerRole.Programmer, core) && isUpToManagerLimit;
        bool hasTeams = product.team.Teams.Count > 1;

        Draw(Hire, true); // and has enough MP
        Draw(ManagerFocus, hasLeadManager);

        AddNewTeam.SetProgress(progress);
        Draw(AddNewTeam, isCoreTeam && (grownUp || hasTeams));

        Draw(DetachTeam, !isCoreTeam);

        Draw(PromoteTeam, false && Flagship.team.Teams[SelectedTeam].Rank < TeamRank.Department);
    }
}
