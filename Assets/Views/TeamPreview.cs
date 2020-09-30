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

    public void SetEntity(TeamInfo info, int teamId)
    {
        this.teamId = teamId;

        var company = Flagship;

        TeamInfo = info;
        var TeamType = info.TeamType;

        //TeamTypeImage.sprite = GetTeamTypeSprite();

        int maxWorkers = 8;
        int workers = info.Workers; // Random.Range(0, maxWorkers);
        bool hasFullTeam = workers >= maxWorkers;

        var hiringProgress = info.HiringProgress;

        HiringProgress.fillAmount = hiringProgress / 100f; // workers / maxWorkers;
        //HiringProgressText.text = hiringProgress + "%";
        HiringProgressText.text = (workers * 100 / maxWorkers) + "%";

        Draw(HiringProgress, !hasFullTeam);
        Draw(HiringProgressText, !hasFullTeam);
        Draw(HiringProgressBackground, !hasFullTeam);

        bool canHireMoreManagers = info.Managers.Count < 2;

        bool hasNoManagerFocus = info.ManagerTasks.Contains(ManagerTask.None);

        bool isFirstTeam = company.team.Teams.Count == 1;

        bool hasLeadManager = Teams.HasMainManagerInTeam(info, Q, company);
        //bool hasNoManager = info.Managers.Count == 0;
        bool hasNoManager = !hasLeadManager;

        GetComponent<Blinker>().enabled = (isFirstTeam && hasNoManagerFocus) || hasNoManager;

        bool hasDisloyalManagers = info.Managers
            .Select(m => Humans.GetHuman(Q, m))
            .Count(h => h.humanCompanyRelationship.Morale < 40 && Teams.GetLoyaltyChangeForManager(h, info, Q) < 0) > 0;

        if (hasFullTeam)
        {
            Draw(NeedToInteract, hasNoManager || hasNoManagerFocus || hasDisloyalManagers);
        }
        else
        {
            Hide(NeedToInteract);
        }


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
