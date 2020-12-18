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
    public GameObject TeamLabel;
    public GameObject Buttons;

    public GameObject PromoteTeamButton;

    public HumanPreview HumanPreview;
    public GameObject HumanLabel;
    
    public List<GameObject> TeamTabs => new List<GameObject> { TeamPanel, TeamLabel };
    public List<GameObject> HumanTabs => new List<GameObject> { HumanPreview.gameObject, HumanLabel };
    
    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var teams = company.team.Teams;
        var teamCount = teams.Count;

        var coreTeam = teams[0];

        var points = 1000;

        const int PROMOTION_POINTS_TO_SMALL_TEAM = 25;
        const int PROMOTION_POINTS_TO_BIG_TEAM = 50;
        const int PROMOTION_POINTS_TO_DEPARTMENT = 150;
        const int PROMOTION_POINTS_TO_SPECIALISED_TEAMS = 250;

        bool showTeamButtons = false;
        bool showHuman = false;
        bool showTeams = false;

        bool canPromote = false;

        if (teamCount == 1)
        {
            if (coreTeam.Rank == TeamRank.Solo)
            {
                showHuman = true;
                
                NextUpgradeGoal.text = $"On {PROMOTION_POINTS_TO_SMALL_TEAM} manager points you will\n{Visuals.Positive("hire your first employees!")}";
                TeamStatusLabel.text = $"<b>You do everything ALONE</b>";

                canPromote = points >= PROMOTION_POINTS_TO_SMALL_TEAM;
            }

            if (coreTeam.Rank == TeamRank.SmallTeam)
            {
                showHuman = true;

                NextUpgradeGoal.text = $"On {PROMOTION_POINTS_TO_BIG_TEAM} manager points you will\n{Visuals.Positive("hire your first MANAGERS!")}";
                TeamStatusLabel.text = $"<b>You have a small team of {coreTeam.Workers}</b>";
                
                canPromote = points >= PROMOTION_POINTS_TO_BIG_TEAM;
            }

            if (coreTeam.Rank == TeamRank.BigTeam)
            {
                showHuman = true;
                showTeams = true;

                NextUpgradeGoal.text = $"On {PROMOTION_POINTS_TO_DEPARTMENT} manager points you will\n{Visuals.Positive("hire more teams!")}";
                TeamStatusLabel.text = $"<b>You have a big team of {coreTeam.Workers}</b>";

                canPromote = points >= PROMOTION_POINTS_TO_DEPARTMENT;
            }

            if (coreTeam.Rank == TeamRank.Department)
            {
                showHuman = true;
                showTeams = true;
                // showTeamButtons = true;

                NextUpgradeGoal.text = $"On {PROMOTION_POINTS_TO_SPECIALISED_TEAMS} manager points you will\n{Visuals.Positive("hire specialised teams!")}";
                TeamStatusLabel.text = $"<b>You have a DEPARTMENT ({coreTeam.Workers} employees)</b>";

                showTeamButtons = points >= PROMOTION_POINTS_TO_SPECIALISED_TEAMS;
            }
            // return $"<b>You have a DEPARTMENT ({coreTeam.Workers} / {Teams.GetMaxTeamSize(coreTeam)}</b>";
        }

        else
        {
            showTeams = true;
            showTeamButtons = true;
            
            TeamStatusLabel.text = "";
            NextUpgradeGoal.text = "";
        }

        if (showTeams)
            ShowAll(TeamTabs);
        else
            HideAll(TeamTabs);
        
        if (showHuman)
            ShowAll(HumanTabs);
        else
            HideAll(HumanTabs);

        Draw(Buttons, showTeamButtons);
        
        ScreenUtils.SetSelectedTeam(Q, 0);
        Draw(PromoteTeamButton, canPromote);
    }
}
