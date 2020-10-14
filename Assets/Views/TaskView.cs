using Assets.Core;
using UnityEngine.UI;

public partial class TaskView : View
{
    public Text Text;
    public ProgressBar ProgressBar;
    public Slider Slider;
    public Image Panel;

    TimedActionComponent TaskComponent;

    public void SetEntity(TimedActionComponent task)
    {
        TaskComponent = task;

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        if (TaskComponent == null)
            return;

        var remaining = TaskComponent.EndTime - CurrentIntDate;

        var progress = (CurrentIntDate - TaskComponent.StartTime) * 100 / (TaskComponent.Duration);

        bool hasProgressbar = ProgressBar != null;
        bool hasSlider = false && Slider != null;



        RenderValue(remaining, progress, hasProgressbar, hasSlider);

        RenderText(remaining, hasProgressbar);

        RenderPanel();
    }

    void RenderValue(int remaining, int progress, bool hasProgressbar, bool hasSlider)
    {
        // render value
        if (hasProgressbar)
        {
            ProgressBar.gameObject.SetActive(remaining > 0);
            ProgressBar.SetValue(progress);
        }

        //// same as Progressbar
        //if (hasSlider)
        //{
        //    Slider.gameObject.SetActive(remaining > 0);
        //    Slider.value = progress;
        //}
    }

    void RenderPanel()
    {
        if (Panel != null)
        {
            if (TaskComponent.StartTime > CurrentIntDate)
                Panel.color = Visuals.GetColorFromString(Colors.COLOR_PANEL_BASE); // not started yet
            else
                Panel.color = Visuals.GetColorFromString(Colors.COLOR_PANEL_SELECTED); // started
        }
    }

    void RenderText(int remaining, bool hasProgressbar)
    {
        var text = GetTaskHeader(TaskComponent.CompanyTask);

        if (remaining <= 0)
        {
            text += "\n\nDONE";
        }
        else
        {
            if (hasProgressbar)
                text += $" ({remaining}d)";
            else
                text += $"\n\n{remaining} days left";

            if (!ScheduleUtils.IsTimerRunning(Q) && !hasProgressbar)
                text += ". " + Visuals.Negative("Unpause") + " to finish";
        }

        Text.text = text;
    }
}


public partial class TaskView : View
{
    string GetTaskHeader(CompanyTask companyTask)
    {
        switch (companyTask.CompanyTaskType)
        {
            case CompanyTaskType.AcquiringCompany:
                return "Acquiring company\n" + Companies.GetName(Q, companyTask.CompanyId);

            case CompanyTaskType.ExploreMarket:
                return "Exploring new market\n" + Enums.GetFormattedNicheName((companyTask as CompanyTaskExploreMarket).NicheType);

            case CompanyTaskType.ExploreCompany:
                return "Exploring company\n" + Companies.GetName(Q, companyTask.CompanyId);

            case CompanyTaskType.UpgradeFeature:
                return $"Adding {(companyTask as CompanyTaskUpgradeFeature).ProductImprovement} feature\n";

            default: return "" + companyTask.CompanyTaskType;
        }
    }
}
