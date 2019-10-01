using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskView : View
{
    public Text Text;

    public void SetEntity(TaskComponent task)
    {
        var text = GetTaskHeader(task.CompanyTask) + "\n\n";

        var remaining = task.EndTime - CurrentIntDate;

        if (remaining <= 0)
            text += "DONE";
        else
            text += "Finished in\n" + remaining + " days";

        Text.text = text;
    }

    string GetTaskHeader(CompanyTask companyTask)
    {
        switch (companyTask.CompanyTaskType)
        {
            case CompanyTaskType.AcquiringCompany:
                return "Acquiring company\n" + CompanyUtils.GetCompanyById(GameContext, (companyTask as CompanyTaskAcquisition).CompanyId).company.Name;

            case CompanyTaskType.ExploreMarket:
                return "Exploring new market\n" + EnumUtils.GetFormattedNicheName((companyTask as CompanyTaskExploreMarket).NicheType);

            default: return "UNKNOWN TASK!!!!" + companyTask.CompanyTaskType;
        }
    }
}


public enum CompanyTaskType
{
    ExploreMarket,
    ExploreCompany,

    AcquiringCompany,
    AcquiringParlay,


}

public class CompanyTask
{
    public CompanyTaskType CompanyTaskType;
}

public class CompanyTaskAcquisition : CompanyTask
{
    public int CompanyId;

    public CompanyTaskAcquisition(int companyId)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.AcquiringCompany;
    }
}

public class CompanyTaskExploreMarket : CompanyTask
{
    public NicheType NicheType;

    public CompanyTaskExploreMarket(NicheType nicheType)
    {
        CompanyTaskType = CompanyTaskType.ExploreMarket;
        NicheType = nicheType;
    }
}
