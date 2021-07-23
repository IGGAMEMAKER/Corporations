using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamButtonsController : View
{
    public EventView AddNewTeam;
    public EventView DetachTeam;
    public EventView PromoteTeam;

    public override void ViewRender()
    {
        var progress = 15;

        var core = Flagship.team.Teams[0];

        var mp = Flagship.companyResource.Resources.managerPoints;
        var needToCreate = Teams.GetPromotionCost(TeamRank.Solo);

        progress = Mathf.Clamp(mp, 0, needToCreate) * 100 / needToCreate;

        AddNewTeam.SetProgress(progress);
        Draw(AddNewTeam, core.Rank == TeamRank.Department);

        Draw(DetachTeam, core.Rank >= TeamRank.BigTeam);

        Draw(PromoteTeam, Flagship.team.Teams[SelectedTeam].Rank < TeamRank.Department);
    }

    void OnEnable()
    {
        
    }
}
