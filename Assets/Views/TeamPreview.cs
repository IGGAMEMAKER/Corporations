using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeamPreview : View
{
    public TeamInfo TeamInfo;
    public int teamId;

    public Image HiringProgress;
    public Image HiringProgressBackground;
    public Text HiringProgressText;

    public Image NeedToInteract;

    public RawImage TeamIcon;

    public Sprite UniversalIcon;
    public Sprite DevelopmentIcon;
    public Sprite MarketingIcon;
    public Sprite ServersideIcon;

    public void SetEntity(TeamInfo team, int teamId)
    {
        this.teamId = teamId;

        var company = Flagship;

        TeamInfo = team;

        // show hiring progress
        int maxWorkers = 8;
        int workers = team.Workers;
        bool hasFullTeam = Teams.IsFullTeam(team);

        var hiringProgress = team.HiringProgress;

        HiringProgress.fillAmount = hiringProgress / 100f;
        HiringProgressText.text = (workers * 100 / maxWorkers) + "%";

        Draw(HiringProgress, !hasFullTeam);
        Draw(HiringProgressText, !hasFullTeam);
        Draw(HiringProgressBackground, !hasFullTeam);


        // need to interact with team
        Draw(NeedToInteract, Teams.IsTeamNeedsAttention(company, team, Q));

        // blink if it's first team
        bool isFirstTeam = company.team.Teams.Count == 1;
        bool hasNoManagerFocus = team.ManagerTasks.Contains(ManagerTask.None);
        bool hasNoManager = Teams.NeedsMainManagerInTeam(team, Q, company);
        GetComponent<Blinker>().enabled = (isFirstTeam && hasNoManagerFocus) || hasNoManager;

        // choose team icon
        switch (TeamInfo.TeamType)
        {
            case TeamType.DevelopmentTeam:
                TeamIcon.texture = DevelopmentIcon.texture;
                break;

            case TeamType.MarketingTeam:
                TeamIcon.texture = MarketingIcon.texture;
                break;

            case TeamType.DevOpsTeam:
                TeamIcon.texture = ServersideIcon.texture;
                break;

            default:
                TeamIcon.texture = UniversalIcon.texture;
                break;
        }
    }
}
