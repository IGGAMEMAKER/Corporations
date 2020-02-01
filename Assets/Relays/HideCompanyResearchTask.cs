using Assets.Core;

public class HideCompanyResearchTask : HideTaskView
{
    public override TaskComponent GetTask()
    {
        return Cooldowns.GetTask(Q, new CompanyTaskExploreCompany(SelectedCompany.company.Id));
    }
}
