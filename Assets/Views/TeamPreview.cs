using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class TeamPreview : View
{
    public TeamInfo TeamInfo;

    public Image HiringProgress;
    public Image HiringProgressBackground;
    public Text HiringProgressText;

    public Image NeedToInteract;

    public RawImage TeamIcon;

    public Sprite UniversalIcon;
    public Sprite DevelopmentIcon;
    public Sprite MarketingIcon;
    public Sprite ServersideIcon;

    public void SetEntity(TeamInfo team)
    {
        var company = Flagship;

        TeamInfo = team;

        // show hiring progress
        int maxWorkers = Teams.GetMaxTeamSize(team);
        int workers = team.Workers;
        bool hasFullTeam = Teams.IsFullTeam(team);

        RenderHiringProgress(company, team, workers, maxWorkers, hasFullTeam);

        // blink if never interacted with teams
        bool hasNoManager = Teams.NeedsMainManagerInTeam(team);

        GetComponent<Blinker>().enabled = Teams.IsNeverHiredEmployees(company) || hasNoManager;

        RenderTeamImage();

        RenderTeamHint(company, team, hasFullTeam, workers, maxWorkers);
    }

    void RenderHiringProgress(GameEntity company, TeamInfo team, int workers, int maxWorkers, bool hasFullTeam)
    {
        var hiringProgress = team.HiringProgress;

        HiringProgress.fillAmount = hiringProgress / 100f;
        HiringProgressText.text = (workers * 100 / maxWorkers) + "%";

        Draw(HiringProgress, !hasFullTeam);
        Draw(HiringProgressText, !hasFullTeam);
        Draw(HiringProgressBackground, !hasFullTeam);


        // need to interact with team
        Draw(NeedToInteract, Teams.IsTeamNeedsAttention(company, team, Q));
    }

    void RenderTeamHint(GameEntity company, TeamInfo team, bool hasFullTeam, int workers, int maxWorkers)
    {
        var hint = $"<size=35>{team.Name}</size>\n";

        if (!hasFullTeam)
        {
            hint += $"\nIs hiring employees: {workers} / {maxWorkers}";
        }
        else
        {
            hint += $"\nCurrently has {workers} employees";
        }

        // hint += "\n\nCan perform: ";
        //
        // var devTasks = Teams.GetSlotsForTask(team, Teams.GetDevelopmentTaskMockup());
        // var marketingTasks = Teams.GetSlotsForTask(team, Teams.GetMarketingTaskMockup());
        //
        // if (devTasks > 0)
        // {
        //     hint += $"\n{Visuals.Positive(devTasks.ToString())} development tasks";
        // }
        //
        // if (marketingTasks > 0)
        // {
        //     hint += $"\n{Visuals.Positive(marketingTasks.ToString())} marketing tasks";
        // }

        var bonus = Teams.GetTeamManagementBonus(team, company, Q, true);
        var bonusSum = bonus.Sum();
        
        hint += Visuals.Colorize($"\n\n<b>Manager points ({bonusSum})</b>\n\n", bonusSum >= 0) + bonus.Minify().ToString();

        // render hint
        GetComponent<Hint>().SetHint(hint);
    }

    void RenderTeamImage()
    {
        // choose team icon
        switch (TeamInfo.TeamType)
        {
            case TeamType.DevelopmentTeam:
                TeamIcon.texture = DevelopmentIcon.texture;
                break;

            case TeamType.MarketingTeam:
                TeamIcon.texture = MarketingIcon.texture;
                break;

            case TeamType.ServersideTeam:
                TeamIcon.texture = ServersideIcon.texture;
                break;

            default:
                TeamIcon.texture = UniversalIcon.texture;
                break;
        }
    }
}
