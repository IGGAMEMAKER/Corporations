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

    public void SetEntity(TeamInfo info, int teamId, TeamTask task = null)
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

        if (task != null)
        {
            RenderTaskAssignTeamView(task, info);
        }
        else
        {
            RenderDefaultTeamView(chosenSlots, info);
        }
    }

    void RenderTaskAssignTeamView(TeamTask task, TeamInfo info)
    {
        if (task.IsFeatureUpgrade)
        {
            var maxLevel = Products.GetFeatureRatingCap(Flagship); // Random.Range(4, 10);
            var gain = Products.GetFeatureRatingGain(Flagship, info);

            Text1.text = Visuals.Positive(gain.ToString("+0.0")); // from organisation

            Text2.text = $"{maxLevel.ToString("0.0")} lvl";
            Text2.color = Visuals.GetGradientColor(0, 10, maxLevel);

            Text3.text = $"";
        }

        if (task.IsMarketingTask)
        {
            var marketingEffeciency = Teams.GetMarketingEfficiency(Flagship);
            var channel = Markets.GetMarketingChannel(Q, (task as TeamTaskChannelActivity).ChannelId);

            var baseGain = Marketing.GetChannelClientGain(Flagship, channel);
            var finalGain = baseGain * marketingEffeciency / 100;

            Text1.text = $"{marketingEffeciency}%";
            Text1.color = Visuals.GetGradientColor(50, 150, marketingEffeciency);

            Text2.text = $"+{Format.Minify(finalGain)} users"; // from organisation
            Text2.color = Visuals.Positive(); // Visuals.GetGradientColor(50, 150, marketingEffeciency);

            Text3.text = $"";
            Text3.color = Visuals.Neutral();
        }

        if (task.IsHighloadTask)
        {
            Text1.text = $"";
            //Text1.color = Visuals.GetGradientColor(5, 20, iterationSpeed);
            Text1.color = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);

            Text2.text = $"";
            Text2.color = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);

            Text3.text = $"";
            Text3.color = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);
        }

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
            case TeamType.ServersideTeam: return DevopsTeam;
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
}
