using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TeamView : View/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public int teamId;
    int freeSlots;

    public Text TeamName;
    public TeamType TeamType;

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

    public Text Text1;
    public Text Text2;
    public Text Text3;
    public Text Text4;

    public void SetEntity(TeamInfo info, int teamId, bool isTaskAssignMode)
    {
        this.teamId = teamId;

        var max = C.TASKS_PER_TEAM;

        var company = Flagship;

        var chosenSlots = company.team.Teams[teamId].Tasks.Count;
        freeSlots = max - chosenSlots;

        TeamInfo = info;
        TeamType = info.TeamType;

        TeamName.text = info.Name;

        ChooseHireManagersOfTeam.SetEntity(teamId);
        TeamTypeImage.sprite = GetTeamTypeSprite();

        if (isTaskAssignMode)
        {
            RenderTaskAssignTeamView();
        }
        else
        {
            RenderDefaultTeamView(chosenSlots, info);
        }
    }

    void RenderTaskAssignTeamView()
    {
        var iterationSpeed = Random.Range(5, 20);
        var expertise = Random.Range(0, 100);
        var maxLevel = Random.Range(4, 10);
        
        Text1.text = $"{iterationSpeed} days"; // from organisation
        Text1.color = Visuals.GetGradientColor(5, 20, iterationSpeed);

        Text2.text = $"{maxLevel} lvl";
        Text2.color = Visuals.GetGradientColor(0, 10, maxLevel);

        Text3.text = $"{expertise}%";
        Text3.color = Visuals.GetGradientColor(0, 100, expertise);

        Text4.text = $"{(freeSlots == 0 ? "NO" : freeSlots.ToString())}";
        Text4.color = Visuals.GetColorPositiveOrNegative(freeSlots > 0);
    }

    void RenderDefaultTeamView(int chosenSlots, TeamInfo info)
    {
        AddTeamTaskListView.FreeSlots = freeSlots;
        AddTeamTaskListView.SetEntity(teamId);

        TeamTaskListView.ChosenSlots = chosenSlots;
        TeamTaskListView.SetEntity(teamId);

        RenderTasks();

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

    void RenderTasks()
    {
        Hide(AddTeamTaskListView);
        Show(TeamTaskListView);
    }

    void HideTasks()
    {
        Hide(AddTeamTaskListView);
        Hide(TeamTaskListView);
    }

    //void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    //{
    //    RenderTasks();
    //}

    //void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    //{
    //    if (freeSlots == 0)
    //        HideTasks();
    //}
}
