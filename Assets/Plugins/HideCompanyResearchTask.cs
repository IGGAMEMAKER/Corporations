using Assets.Utils;

public class HideCompanyResearchTask : HideTaskView
{
    public override TaskComponent GetTask()
    {
        return CooldownUtils.GetTask(GameContext, new CompanyTaskExploreCompany(SelectedCompany.company.Id));
    }
}
