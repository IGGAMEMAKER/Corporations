using System.Collections;
using System.Collections.Generic;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class TeamStatusView : View
{
    public Text NextUpgradeGoal;
    public Text TeamStatusLabel;
    public GameObject TeamPanel;
    // public GameObject TeamLabel;
    public GameObject Buttons;

    public GameObject PromoteTeamButton;

    public HumanPreview HumanPreview;
    public GameObject HumanLabel;
    
    public List<GameObject> TeamTabs => new List<GameObject> { TeamPanel };
    public List<GameObject> HumanTabs => new List<GameObject> { HumanPreview.gameObject, HumanLabel };
    
    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var teams = company.team.Teams;
        var teamCount = teams.Count;

        var coreTeam = teams[0];

        var points = company.companyResource.Resources.managerPoints;



        bool showTeamButtons = false;
        bool showTeams = false;

        bool canPromote = false;

        if (teamCount == 1)
        {
            var promotionCost = Teams.GetPromotionCost(Teams.GetNextTeamRank(coreTeam.Rank));

            if (coreTeam.Rank == TeamRank.Solo)
            {
                NextUpgradeGoal.text = $"On {promotionCost} manager points you will\n{Visuals.Positive("hire your first employees!")}";
                TeamStatusLabel.text = $"<b>You do everything ALONE</b>";

                canPromote = points >= promotionCost;
            }

            if (coreTeam.Rank == TeamRank.SmallTeam)
            {
                NextUpgradeGoal.text = $"On {promotionCost} manager points you will\n{Visuals.Positive("hire your first MANAGERS!")}";
                TeamStatusLabel.text = $"<b>You have a small team of {coreTeam.Workers}</b>";
                
                canPromote = points >= promotionCost;
            }

            if (coreTeam.Rank == TeamRank.BigTeam)
            {
                showTeams = true;

                NextUpgradeGoal.text = $"On {promotionCost} manager points you will\n{Visuals.Positive("hire more teams!")}";
                TeamStatusLabel.text = $"<b>You have a big team of {coreTeam.Workers}</b>";

                canPromote = points >= promotionCost;
                showTeamButtons =
                    Flagship.companyGoal.Goals.Exists(g =>
                        g.InvestorGoalType == InvestorGoalType.ProductPrepareForRelease) ||
                    Flagship.completedGoals.Goals.Exists(g => g == InvestorGoalType.ProductPrepareForRelease);
            }

            if (coreTeam.Rank == TeamRank.Department)
            {
                showTeams = true;
                promotionCost = C.PROMOTION_POINTS_TO_SPECIALISED_TEAMS;

                NextUpgradeGoal.text = $"On {promotionCost} manager points you will\n{Visuals.Positive("hire specialised teams!")}";
                TeamStatusLabel.text = $"<b>You have a DEPARTMENT ({coreTeam.Workers} employees)</b>";

                showTeamButtons = true;
                // showTeamButtons = points >= promotionCost;
            }
        }

        else
        {
            showTeams = true;
            showTeamButtons = true;

            NextUpgradeGoal.text = "";
            TeamStatusLabel.text = "Teams";
        }

        showTeamButtons = showTeamButtons || canPromote;

        if (showTeams)
            ShowAll(TeamTabs);
        else
            HideAll(TeamTabs);

        ShowAll(HumanTabs);


        Draw(Buttons, showTeamButtons);
        
        ScreenUtils.SetSelectedTeam(Q, 0);
        Draw(PromoteTeamButton, canPromote);
    }
}
