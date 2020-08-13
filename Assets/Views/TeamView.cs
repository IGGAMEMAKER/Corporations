using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TeamView : View, IPointerEnterHandler, IPointerExitHandler
{
    public Text TeamName;
    public TeamType TeamType;
    public GameObject RemoveTeam;

    public TeamInfo TeamInfo;

    public AddTeamTaskListView AddTeamTaskListView;
    public TeamTaskListView TeamTaskListView;

    public Image TeamTypeImage;

    [Header("Team images")]
    public Sprite CoreTeam;
    public Sprite SmallTeam;
    public Sprite UniversalTeam;
    public Sprite BigTeam;
    public Sprite MarketingTeam;
    public Sprite DevelopmentTeam;
    public Sprite SupportTeam;
    public Sprite DevopsTeam;

    public ProgressBar ProgressBar;

    public ChooseHireManagersOfTeam ChooseHireManagersOfTeam;

    public void SetEntity(TeamInfo info, int teamId)
    {
        TeamInfo = info;
        TeamType = info.TeamType;

        RemoveTeam.GetComponent<RemoveTeamController>().TeamId = teamId;
        TeamName.text = info.Name;

        ChooseHireManagersOfTeam.SetEntity(teamId);

        var max = C.TASKS_PER_TEAM;

        var company = Flagship;

        var chosenSlots = company.team.Teams[teamId].Tasks.Count;
        var freeSlots = max - chosenSlots;

        AddTeamTaskListView.FreeSlots = freeSlots;
        AddTeamTaskListView.SetEntity(teamId);

        TeamTaskListView.ChosenSlots = chosenSlots;
        TeamTaskListView.SetEntity(teamId);

        TeamTypeImage.sprite = GetTeamTypeSprite();

        int maxWorkers = 8;
        int workers = info.Workers; // Random.Range(0, maxWorkers);
        bool hasFullTeam = workers >= maxWorkers;

        var hiringProgress = info.HiringProgress;

        ProgressBar.SetDescription("Hiring workers");
        ProgressBar.SetValue(hiringProgress, 100);
        ProgressBar.SetCustomText($"{workers} / {maxWorkers}");

        Draw(ProgressBar, !hasFullTeam);
    }

    Sprite GetTeamTypeSprite()
    {
        switch (TeamType)
        {
            case TeamType.BigCrossfunctionalTeam: return BigTeam;
            case TeamType.CoreTeam: return CoreTeam;
            case TeamType.CrossfunctionalTeam: return UniversalTeam;
            case TeamType.DevelopmentTeam: return DevelopmentTeam;
            case TeamType.DevOpsTeam: return DevopsTeam;
            case TeamType.MarketingTeam: return MarketingTeam;
            case TeamType.SmallCrossfunctionalTeam: return SmallTeam;
            case TeamType.SupportTeam: return SupportTeam;

            default: return CoreTeam;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Draw(RemoveTeam, false);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Draw(RemoveTeam, false);
    }
}
