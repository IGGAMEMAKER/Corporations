using Assets.Core;
using Assets.Core.Formatting;
using UnityEngine.UI;

public partial class TaskView : View
{
    public Text Text;
    public ProgressBar ProgressBar;
    public Image Panel;

    TaskComponent TaskComponent;

    public void SetEntity(TaskComponent task)
    {
        TaskComponent = task;
        AddLinkToObservableObject(task.CompanyTask);

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        var text = GetTaskHeader(TaskComponent.CompanyTask);

        var remaining = TaskComponent.EndTime - CurrentIntDate;

        var progress = (CurrentIntDate - TaskComponent.StartTime) * 100 / (TaskComponent.Duration);

        bool hasProgressbar = ProgressBar != null;

        if (hasProgressbar)
        {
            ProgressBar.gameObject.SetActive(remaining > 0);
            ProgressBar.SetValue(progress);
        }

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

        if (Panel != null)
        {
            if (TaskComponent.StartTime > CurrentIntDate)
                Panel.color = Visuals.GetColorFromString(Colors.COLOR_PANEL_BASE); // not started yet
            else
                Panel.color = Visuals.GetColorFromString(Colors.COLOR_PANEL_SELECTED); // started
        }


        Text.text = text;
    }

    private void AddLinkToObservableObject(CompanyTask companyTask)
    {
        //switch (companyTask.CompanyTaskType)
        //{
        //    case CompanyTaskType.ExploreMarket:
        //        AddIfAbsent<LinkToNiche>().SetNiche((companyTask as CompanyTaskExploreMarket).NicheType);
        //        break;

        //    case CompanyTaskType.ExploreCompany:
        //        AddIfAbsent<LinkToProjectView>().CompanyId = ((companyTask as CompanyTaskExploreCompany).CompanyId);
        //        break;

        //    case CompanyTaskType.AcquiringCompany:
        //        AddIfAbsent<LinkToProjectView>().CompanyId = ((companyTask as CompanyTaskAcquisition).CompanyId);
        //        break;
        //}
    }
}

public partial class TaskView : View
{
    string GetTaskHeader(CompanyTask companyTask)
    {
        switch (companyTask.CompanyTaskType)
        {
            case CompanyTaskType.AcquiringCompany:
                return "Acquiring company\n" + Companies.GetCompanyName(Q, companyTask.CompanyId);

            case CompanyTaskType.ExploreMarket:
                return "Exploring new market\n" + EnumUtils.GetFormattedNicheName((companyTask as CompanyTaskExploreMarket).NicheType);

            case CompanyTaskType.ExploreCompany:
                return "Exploring company\n" + Companies.GetCompanyName(Q, companyTask.CompanyId);

            case CompanyTaskType.UpgradeFeature:
                return $"Adding {(companyTask as CompanyTaskUpgradeFeature).ProductImprovement} feature\n";

            default: return "UNKNOWN TASK!!!!" + companyTask.CompanyTaskType;
        }
    }
}
