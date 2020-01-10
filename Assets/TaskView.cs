using Assets.Core;
using Assets.Core.Formatting;
using UnityEngine.UI;

public partial class TaskView : View
{
    public Text Text;

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
        var text = GetTaskHeader(TaskComponent.CompanyTask) + "\n\n";

        var remaining = TaskComponent.EndTime - CurrentIntDate;

        if (remaining <= 0)
        {
            text += "DONE";
        }
        else
        {
            text += remaining + " days left";
            if (!ScheduleUtils.IsTimerRunning(GameContext))
                text += ". " + Visuals.Negative("Unpause") + " to finish";
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
                return "Acquiring company\n" + Companies.GetCompany(GameContext, (companyTask as CompanyTaskAcquisition).CompanyId).company.Name;

            case CompanyTaskType.ExploreMarket:
                return "Exploring new market\n" + EnumUtils.GetFormattedNicheName((companyTask as CompanyTaskExploreMarket).NicheType);

            case CompanyTaskType.ExploreCompany:
                return "Exploring company\n" + Companies.GetCompany(GameContext, (companyTask as CompanyTaskExploreCompany).CompanyId).company.Name;

            case CompanyTaskType.UpgradeFeature:
                return $"Adding {(companyTask as CompanyTaskUpgradeFeature).ProductImprovement} feature\n";

            default: return "UNKNOWN TASK!!!!" + companyTask.CompanyTaskType;
        }
    }
}
