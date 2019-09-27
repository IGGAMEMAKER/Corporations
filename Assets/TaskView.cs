using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskView : View
{
    public Text Text;

    public void SetEntity(CompanyTask companyTask)
    {
        var text = GetTaskHeader(companyTask);

        var remaining = companyTask.EndDate - CurrentIntDate;

        if (remaining == 0)
            text += "DONE";
        else
            text += "\n\nWill be finished in\n" + remaining + " days";

        Text.text = text;
    }

    string GetTaskHeader(CompanyTask companyTask)
    {
        switch (companyTask.CompanyTaskType)
        {
            case CompanyTaskType.AcquiringCompany:
                return "Acquiring company\nFacebook";

            case CompanyTaskType.ExploreMarket:
                return EnumUtils.GetFormattedNicheName((companyTask as CompanyTaskExploreMarket).NicheType);

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
    public int EndDate;
    public CompanyTaskType CompanyTaskType;
}

public class CompanyTaskAcquisition : CompanyTask
{
    public CompanyTaskAcquisition()
    {
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
