using Assets.Core;

public class HideCompanyResearchTask : HideTaskView
{
    public override TimedActionComponent GetTask()
    {
        return Cooldowns.GetTask(Q, new CompanyTaskExploreCompany(SelectedCompany.company.Id));
    }
}
